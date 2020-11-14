using MRI.Core;
using Spire.Pdf;
using Spire.Pdf.Exporting;
using Spire.Pdf.General.Find;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

namespace MRI.AssignCoord
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Text { get; private set; } = "this is set";

        public DataTable BookTable { get; private set; } = new DataTable();

        public DataTable PageTable { get; private set; } = new DataTable();

        public DataTable PageItemTable { get; private set; } = new DataTable();

        public DataTable PartTable { get; private set; } = new DataTable();

        public MainWindowViewModel()
        {
            LoadTablesFromSql();


        }

        private void LoadTablesFromSql()
        {
            BookTable = SqlStaticHelpers.ReadTableData("Book");
            PageTable = SqlStaticHelpers.ReadTableData("Page");
            PageItemTable = SqlStaticHelpers.ReadTableData("PageItem");
            PartTable = SqlStaticHelpers.ReadTableData("Part");
        }

        //private void SavedShit()
        //{
        //    PdfDocument doc = new PdfDocument();
        //    //doc.LoadFromFile(@"D:\source\repos\GitHubTeamRepos\MotorcycleManual\MotorcycleRepairInteractive\MRI\PDFFiles\F.pdf");
        //    doc.LoadFromFile(@"D:\source\repos\GitHubTeamRepos\MotorcycleManual\MotorcycleRepairInteractive\MRI\PDFFiles\Pg8CrankshaftAndConnectingRods.pdf");


        //    StringBuilder content = new StringBuilder();
        //    content.Append(doc.Pages[0].ExtractText());



        //    var otherresult = doc.Pages[0].FindAllText();

        //    foreach (var t in otherresult.Finds)
        //    {
        //        var Number = t.SearchText;
        //        PointF point = t.Position;
        //        float x = point.X;
        //        float y = point.Y;
        //        float width = t.Size.Width;
        //        float height = t.Size.Height;

        //    }



        //    var q = new BookDataModel(SqlStaticHelpers.ReadRowData());

        //    SqlStaticHelpers.ReadRowData();

        //    string connetionString;
        //    SqlConnection cnn;
        //    connetionString = @"Data Source=LAPTOP-GE9KQ5E6;Initial Catalog=MotorcycleParts;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        //    cnn = new SqlConnection(connetionString);
        //    cnn.Open();
        //    SqlCommand command;
        //    SqlDataReader dataReader;
        //    string sql, Output = "";

        //    sql = "Select Id, PartNumber from dbo.PageItem";

        //    command = new SqlCommand(sql, cnn);

        //    dataReader = command.ExecuteReader();



        //    foreach (var dr in dataReader)
        //    {
        //        var x = dr;

        //    }

        //    cnn.Close();

        //    var result = doc.Pages[25].ExtractText();
        //    //foreach (pdftex find in otherresult)
        //    //{
        //    //    PointF point = find.Position;
        //    //    float x = point.X;
        //    //    float y = point.Y;
        //    //    float width = find.Size.Width;
        //    //    float height = find.Size.Height;
        //    //}

        //    PdfImageInfo[] imageinfos = doc.Pages[0].ImagesInfo;
        //    RectangleF rect = imageinfos[0].Bounds;
        //}
    }
}


