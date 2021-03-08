using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;
using Models.Entities;
using Models.Interfaces.Entities;
// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace MRI.Db
{
  /// <summary>
  /// Provider of API method calls to the Database
  /// </summary>
  public class APIProvider
    : IAPIProvider
  {
    #region Fields

    private readonly GrpcChannel m_channel;
    private readonly Provider.ProviderClient m_client;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    public APIProvider()
      : this(GrpcChannel.ForAddress("https://localhost:5001"))
    {
    }

    /// <summary>
    /// Provider constructor
    /// </summary>
    /// <param name="channel">Grpc channel instance</param>
    protected APIProvider(GrpcChannel channel)
    {
      m_channel = channel;
      m_client = new Provider.ProviderClient(m_channel);
    }

    #region Helpers

    private static IdRequest ToIdRequest(int id)
      => new()
      {
        Id = id
      };

    private static PageParams ToPageParams(int size, int index)
      => new()
      {
        Index = index,
        Size = size
      };

    private static IdSearchAndPageParams ToIdSearchAndPageParams(int id, string? search, int size, int index)
      => new()
      {
        Id = id,
        Index = index,
        Search = search ?? string.Empty,
        Size = size
      };

    // Uncomment if needed
    // private static IdAndPageParams ToIdAndPageParams(int id, int size, int index)
    //   => new()
    //   {
    //     Id = id,
    //     Index = index,
    //     Size = size
    //   };

    private static SearchAndPageParams ToSearchAndPageParams(string? search, int size, int index)
      => new()
      {
        Index = index,
        Search = search ?? string.Empty,
        Size = size
      };

    private async IAsyncEnumerable<TReplyModel> GetItemsAsync<TReply, TReplyModel>(Func<Provider.ProviderClient, AsyncServerStreamingCall<TReply>> extractor, Func<TReply, TReplyModel> converter, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
      var result = extractor(m_client);

      await foreach (var item in result.ResponseStream.ReadAllAsync(cancellationToken).WithCancellation(cancellationToken).ConfigureAwait(false))
        yield return converter(item);
    }

    private async Task<IPaging<TReplyModel>> GetPagingAsync<TReply, TReplyModel>(
      Func<Provider.ProviderClient, AsyncServerStreamingCall<TReply>> extractor, Func<TReply, TReplyModel> converter,
      CancellationToken cancellationToken)
      where TReplyModel : IReply
    {
      var result = extractor(m_client);
      var items = await result.ResponseStream.ReadAllAsync(cancellationToken)
        .Select(converter)
        .Where(x => x is not null)
        .ToListAsync(cancellationToken).ConfigureAwait(false);

      var metadata = result.GetTrailers();
      var (total, size, index) = metadata is null ? (0, 1, 1) : RetrievePaging(metadata);

      return new Paging<TReplyModel>(items, total, size, index);
    }

    private static (int total, int size, int index) RetrievePaging(Metadata metadata)
    {
      int.TryParse(metadata.GetValue("totalsize"), out var totalSize);
      int.TryParse(metadata.GetValue("pagesize"), out var size);
      int.TryParse(metadata.GetValue("pageindex"), out var index);
      return (totalSize, size, index);
    }

    private static IBook ToBook(BookReply reply)
      => new BookModel
      {
        Id = reply.Id,
        Title = reply.Title
      };

    private static IPart ToPart(PartReply reply)
      => new PartModel
      {
        Id = reply.Id,
        PartNumber = reply.PartNumber,
        MakersPartNumber = reply.MakersPartNumber,
        Description = reply.Description
      };

    private static IProperty ToProperty(PartPropertyReply reply)
      => new PropertyModel
      {
        PropertyValue = reply.Value,
        PropertyName = reply.Name,
        Unit = reply.Unit,
        Type = reply.Type,
        PropertyTypeId = reply.TypeId
      };

    private static IPropertyType ToPropertyType(PropertyTypeReply reply)
      => new PropertyTypeModel
      {
        Id = reply.Id,
        Name = reply.Name,
        Unit = reply.Unit
      };

    private static ISection ToSection(SectionReply reply)
      => new SectionModel
      {
        Id = reply.Id,
        BookId = reply.BookId,
        Header = reply.Header,
        StartPage = reply.StartPage,
        EndPage = reply.EndPage,
        FigureNumber = reply.FigureNumber,
        FigureDescription = reply.FigureDescription
      };

    private static ISectionPart ToSectionParts(SectionPartReply reply)
    {
      static ISectionPart Convert(SectionPartReply item, bool children)
        => new SectionPartModel
        {
          Id = item.Part.Id,
          PageNumber = item.PageNumber,
          Remarks = item.Remarks,
          AdditionalInfo = item.AdditionalInfo,
          Quantity = item.Quantity,
          Description = item.Part.Description,
          PartNumber = item.Part.PartNumber,
          MakersPartNumber = item.Part.MakersPartNumber,
          Children = children ? item.Children.Select(x => Convert(x, false)).ToArray() : Array.Empty<ISectionPart>()
        };

      return Convert(reply, true);
    }

    private static IMake ToMake(MakeReply reply)
      => new MakeModel
      {
        Id = reply.Id,
        Name = reply.Name
      };

    private static IModel ToModel(ModelReply reply)
      => new ModelModel
      {
        Id = reply.Id,
        BookId = reply.BookId,
        MakeId = reply.MakeId,
        EngineId = reply.EngineId,
        Name = reply.Name,
        Year = reply.Year
      };

    private static ICarburetor ToCarburetor(CarburetorReply reply)
      => new CarburetorModel
      {
        Id = reply.Id,
        Name = reply.Name
      };

    private static IEngine ToEngine(EngineReply reply)
      => new EngineModel
      {
        Id = reply.Id,
        Name = reply.Name,
        Transmission = reply.Transmission,
        Carburetor = reply.CarburetorName,
        Carburetors = reply.Carburetors,
        Displacement = reply.Displacement
      };

    #endregion

    #region API

    /// <inheritdoc />
    public async ValueTask<IBook> GetBookAsync(int id, CancellationToken cancellationToken = default)
      => ToBook(await m_client.GetBookAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPaging<IBook>> GetBooksAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetBooks(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToBook, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public IAsyncEnumerable<IModel> GetBookModelsAsync(int bookId, CancellationToken cancellationToken = default)
      => GetItemsAsync(client => client.GetBookModels(ToIdRequest(bookId), cancellationToken: cancellationToken), ToModel, cancellationToken);

    /// <inheritdoc />
    public async ValueTask<IMake> GetMakeAsync(int id, CancellationToken cancellationToken = default)
      => ToMake(await m_client.GetMakeAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPaging<IMake>> GetMakesAsync(int size, int index, string? search,
      CancellationToken cancellationToken)
      => await GetPagingAsync(client => client.GetAllMakes(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToMake, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IModel> GetModelAsync(int id, CancellationToken cancellationToken = default)
      => ToModel(await m_client.GetModelAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPaging<IModel>> GetModelsAsync(int size, int index, string? search = default,
      CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllModels(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToModel, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<ICarburetor>> GetCarburetorsAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllCarburetors(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToCarburetor, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IEngine>> GetEnginesAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllEngines(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToEngine, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IEngine> GetModelEngineAsync(int id, CancellationToken cancellationToken = default)
      => ToEngine(await m_client.GetModelEngineAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPart> GetPartAsync(int id, CancellationToken cancellationToken = default)
      => ToPart(await m_client.GetPartAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPaging<ISection>> GetSectionsFromBookAsync(int bookId, int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetSectionsFromBook(ToIdSearchAndPageParams(bookId, search, size, index), cancellationToken: cancellationToken), ToSection, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IPart>> GetPartsAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllParts(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToPart, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IPart>> GetPartsFromBookAsync(int bookId, int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromBook(ToIdSearchAndPageParams(bookId, search, size, index), cancellationToken: cancellationToken), ToPart, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<ISectionPart>> GetPartsFromSectionAsync(int sectionId, int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromSection(ToIdSearchAndPageParams(sectionId, search, size, index), cancellationToken: cancellationToken), ToSectionParts, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IProperty>> GetPartPropertiesAsync(int partId, int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartProperties(ToIdSearchAndPageParams(partId, search, size, index), cancellationToken: cancellationToken), ToProperty, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IModel>> GetSectionSpecificModelsAsync(int sectionId, int size, int index,
      string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetSectionSpecificModels(ToIdSearchAndPageParams(sectionId, search, size, index), cancellationToken: cancellationToken), ToModel, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<ISection> GetSectionAsync(int id, CancellationToken cancellationToken = default)
      => ToSection(await m_client.GetSectionAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async ValueTask<IPaging<ISection>> GetSectionsAsync(int size, int index, string? search = null, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllSections(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToSection, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<ISection>> GetSectionChildrenAsync(int parentId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetSectionChildren(ToIdSearchAndPageParams(parentId, search, size, index), cancellationToken: cancellationToken), ToSection, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc />
    public async ValueTask<IPaging<IPropertyType>> GetPropertyTypesAsync(int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPropertyTypes(ToPageParams(size, index), cancellationToken: cancellationToken), ToPropertyType, cancellationToken).ConfigureAwait(false);

    #endregion

    /// <inheritdoc />
    public void Dispose()
      => m_channel.Dispose();
  }
}