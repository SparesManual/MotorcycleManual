using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Models.BOM;
using MRI.Helpers;

namespace BOM.Infrastructure
{
  /// <summary>
  /// Helper class for processing BOM data
  /// </summary>
  public static class BOMHelper
  {
    #region Fields

    private static readonly XslTransform XSLT;
    private static readonly XmlSchemaSet SCHEMA;

    #endregion

    #region Constants

    private const string XSD = "BOM.Infrastructure.XML.BOM.xsd";
    private const string XSL = "BOM.Infrastructure.XML.BOM.xsl";
    private const string NAMESPACE = "urn:sparesmanual.com:bom";
    private const string PREFIX = "b";
    private const string BILL = "bill";
    private const string CLIENT = "client";
    private const string NAME = "name";
    private const string EMAIL = "email";
    private const string MESSAGE = "message";
    private const string MATERIAL = "material";
    private const string ID = "id";
    private const string QUANTITY = "quantity";
    private const string PART_NUMBER = "partNumber";
    private const string MAKERS_PART_NUMBER = "makersPartNumber";

    #endregion

    static BOMHelper()
    {
      var asm = Assembly.GetExecutingAssembly();
      using var xsd = asm.GetManifestResourceStream(XSD);
      using var xsl = asm.GetManifestResourceStream(XSL);

      using var xsdReader = new StreamReader(xsd!);
      using var xslReader = new XmlTextReader(xsl!);

      XSLT = new XslTransform();
      XSLT.Load(xslReader);

      SCHEMA = new XmlSchemaSet();
      SCHEMA.Add(NAMESPACE, new XmlTextReader(xsdReader));
    }

    #region Methods

    /// <summary>
    /// Converts a BOM to an XML document
    /// </summary>
    /// <param name="bill">BOM data to transform</param>
    /// <returns>BOM data in XML format</returns>
    public static XmlDocument ToXML(this Bill bill)
    {
      var xml = new XmlDocument();
      xml.Schemas = SCHEMA;
      xml.InsertBefore(xml.CreateXmlDeclaration("1.0", "UTF-8", null), xml.DocumentElement);

      var body = xml.CreateElement(PREFIX, BILL, NAMESPACE);
      xml.AppendChild(body);
      if (bill.Client is not null)
      {
        var client = xml.CreateElement(PREFIX, CLIENT, NAMESPACE);

        if (bill.Client.Name is not null)
        {
          var name = xml.CreateElement(PREFIX, NAME, NAMESPACE);
          name.Value = bill.Client.Name;
          client.AppendChild(name);
        }
        if (bill.Client.Email is not null)
        {
          var email = xml.CreateElement(PREFIX, EMAIL, NAMESPACE);
          email.Value = bill.Client.Email;
          client.AppendChild(email);
        }
        if (bill.Client.Message is not null)
        {
          var message = xml.CreateElement(PREFIX, MESSAGE, NAMESPACE);
          message.Value = bill.Client.Message;
          client.AppendChild(message);
        }

        if (client.HasChildNodes)
          body.AppendChild(client);
      }

      foreach (var item in bill.Materials)
      {
        var material = xml.CreateElement(PREFIX, MATERIAL, NAMESPACE);
        material.InnerText = item.Description;

        var id = xml.CreateAttribute(ID, NAMESPACE);
        id.Value = item.Id.ToString();
        material.Attributes.Append(id);

        var partNumber = xml.CreateAttribute(PART_NUMBER, NAMESPACE);
        partNumber.Value = item.PartNumber;
        material.Attributes.Append(partNumber);

        var makersPartNumber = xml.CreateAttribute(MAKERS_PART_NUMBER, NAMESPACE);
        makersPartNumber.Value = item.MakersPartNumber ?? string.Empty;
        material.Attributes.Append(makersPartNumber);

        var quantity = xml.CreateAttribute(QUANTITY, NAMESPACE);
        quantity.Value = item.Quantity.ToString();
        material.Attributes.Append(quantity);

        body.AppendChild(material);
      }

      return xml;
    }

    /// <summary>
    /// Converts an XML document from the given <paramref name="path"/> to BOM data
    /// </summary>
    /// <param name="path">XML file path</param>
    /// <returns>The extracted BOM data</returns>
    /// <exception cref="ArgumentNullException">If the <paramref name="path"/> is null</exception>
    public static Bill ToBill(this string path)
    {
      static Material ToMaterial(XmlNode node)
      {
        if (node is null)
          throw new ArgumentNullException(nameof(node));

        return new Material
        {
          Id = int.Parse(node.Attributes![ID]!.Value),
          Quantity = uint.Parse(node.Attributes[QUANTITY]!.Value),
          PartNumber = node.Attributes[PART_NUMBER]!.Value,
          MakersPartNumber = node.Attributes[MAKERS_PART_NUMBER]!.Value,
          Description = node.Value ?? string.Empty
        };
      }

      static Client ToClient(XmlNode node)
      {
        if (node is null)
          throw new ArgumentNullException(nameof(node));

        return new Client
        {
          Name = node.Attributes![NAME]?.Value,
          Email = node.Attributes[EMAIL]?.Value,
          Message = node.Attributes[MESSAGE]?.Value
        };
      }

      var xml = new XmlDocument();
      xml.Load(path);

      var client = xml.FirstChild?.Name.Equals(CLIENT, StringComparison.InvariantCultureIgnoreCase) ?? false
        ? ToClient(xml.FirstChild)
        : null;
      var materials = xml.ChildNodes
        .Cast<XmlNode>()
        .Where(node => node.Name.Equals(MATERIAL, StringComparison.InvariantCultureIgnoreCase))
        .Select(ToMaterial);

      return new Bill
      {
        Client = client is null || client.Name is null && client.Email is null && client.Message is null ? null : client,
        Materials = materials.ToReadOnlyCollection()
      };
    }

    /// <summary>
    /// Validates whether the given XML file has correct BOM data
    /// </summary>
    /// <param name="path">XML file path</param>
    /// <returns>True if valid</returns>
    public static async ValueTask<bool> Validate(this string path)
    {
      var errors = 0;
      var readerSettings = new XmlReaderSettings
      {
        ValidationType = ValidationType.Schema,
        Schemas = SCHEMA,
        Async = true,
        IgnoreComments = true,
        CloseInput = true
      };
      readerSettings.ValidationEventHandler += (_, args) =>
      {
        if (args.Severity == XmlSeverityType.Error)
          ++errors;
      };

      var reader = XmlReader.Create(path, readerSettings);
      while (await reader.ReadAsync().ConfigureAwait(false) && errors == 0)
      {
      }

      return errors == 0;
    }

    /// <summary>
    /// Converts a BOM to a PDF
    /// </summary>
    /// <param name="bill">BOM data to transform</param>
    /// <param name="output">Output file path</param>
    public static void ToPDF(this Bill bill, string output)
      => bill.ToXML().ToHTML().ToPDF(output);

    /// <summary>
    /// Converts given <paramref name="html"/> data to a PDF
    /// </summary>
    /// <param name="html">HTML data</param>
    /// <param name="output">Output file path</param>
    public static void ToPDF(this string html, string output)
    {
      var document = new Document();
      using var stream = new FileStream(output, FileMode.Create);
      PdfWriter.GetInstance(document, stream);
      document.Open();

      try
      {
        var worker = new HtmlWorker(document);
        using var reader = new StringReader(html);
        worker.Parse(reader);
      }
      finally
      {
        document.Close();
      }
    }

    /// <summary>
    /// Converts a given XML <paramref name="document"/> to HTML
    /// </summary>
    /// <param name="document">XML document to transform</param>
    /// <returns>Converted HTML document</returns>
    public static string ToHTML(this XmlDocument document)
    {
      var builder = new StringBuilder();
      var writer = new XmlTextWriter(new StringWriter(builder))
      {
        Formatting = Formatting.Indented
      };

      XSLT.Transform(document, null, writer, null);

      return builder.ToString();
    }

    /// <summary>
    /// Converts an XML document on a given <paramref name="path"/> to HTML
    /// </summary>
    /// <param name="path">XML document path</param>
    /// <returns>Converted HTML document</returns>
    public static string ToHTML(this string path)
    {
      var builder = new StringBuilder();
      var writer = new XmlTextWriter(new StringWriter(builder))
      {
        Formatting = Formatting.Indented
      };

      XSLT.Transform(new XPathDocument(path), null, writer, null);

      return builder.ToString();
    }

    #endregion
  }
}