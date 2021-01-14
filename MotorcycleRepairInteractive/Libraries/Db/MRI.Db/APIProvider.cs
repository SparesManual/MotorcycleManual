using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;
using Models.Entities;
using Models.Interfaces.Entities;

namespace MRI.Db
{
  /// <summary>
  /// Provider of API method calls to the Database
  /// </summary>
  public sealed class APIProvider
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
    {
      m_channel = GrpcChannel.ForAddress("https://localhost:5001");
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

    private static IdAndPageParams ToIdAndPageParams(int id, int size, int index)
      => new()
      {
        Id = id,
        Index = index,
        Size = size
      };

    private static SearchAndPageParams ToSearchAndPageParams(string? search, int size, int index)
      => new()
      {
        Index = index,
        Search = search ?? string.Empty,
        Size = size
      };

    private async Task<IPaging<TReplyModel>> GetPagingAsync<TReply, TReplyModel>(Func<Provider.ProviderClient, AsyncServerStreamingCall<TReply>> extractor, Func<TReply, TReplyModel> converter)
      where TReplyModel : IReply
    {

      var result = extractor(m_client);
      var headers = await result.ResponseHeadersAsync.ConfigureAwait(false);
      var (total, pageSize, pageIndex) = RetrievePaging(headers);

#pragma warning disable 8424
      async IAsyncEnumerable<TReplyModel> ConvertStream([EnumeratorCancellation] CancellationToken cancellationToken)
      {
        await foreach (var item in result.ResponseStream.ReadAllAsync(cancellationToken).WithCancellation(cancellationToken).ConfigureAwait(false))
          yield return converter(item);
      }
#pragma warning restore 8424

      return new Paging<TReplyModel>(ConvertStream, total, pageSize, pageIndex);
    }

    private static (int total, int size, int index) RetrievePaging(Metadata metadata)
      => (int.Parse(metadata.GetValue("totalsize")), int.Parse(metadata.GetValue("pagesize")), int.Parse(metadata.GetValue("pageindex")));

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

    private static ISectionParts ToSectionParts(SectionPartReply reply)
      => new SectionPartModel
      {
        Id = reply.Id,
        PartId = reply.PartId,
        PageNumber = reply.PageNumber,
        Remarks = reply.Remarks,
        AdditionalInfo = reply.AdditionalInfo,
        Quantity = reply.Quantity
      };

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
    public async Task<IBook> GetBookAsync(int id, CancellationToken cancellationToken = default)
      => ToBook(await m_client.GetBookAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<IBook>> GetBooksAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetBooks(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToBook).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IModel>> GetBookModelsAsync(int bookId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetBookModels(ToIdSearchAndPageParams(bookId, search, size, index), cancellationToken: cancellationToken), ToModel).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IMake> GetMakeAsync(int id, CancellationToken cancellationToken = default)
      => ToMake(await m_client.GetMakeAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<IMake>> GetMakesAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllMakes(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToMake).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IModel> GetModelAsync(int id, CancellationToken cancellationToken = default)
      => ToModel(await m_client.GetModelAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<IModel>> GetModelsAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllModels(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToModel).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<ICarburetor>> GetCarburetorsAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllCarburetors(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToCarburetor).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IEngine>> GetEnginesAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllEngines(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToEngine).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IEngine> GetModelEngineAsync(int id, CancellationToken cancellationToken = default)
      => ToEngine(await m_client.GetModelEngineAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPart> GetPartAsync(int id, CancellationToken cancellationToken = default)
      => ToPart(await m_client.GetPartAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<IPart>> GetPartsAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllParts(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToPart).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IPart>> GetPartsFromBookAsync(int bookId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromBook(ToIdSearchAndPageParams(bookId, search, size, index), cancellationToken: cancellationToken), ToPart).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IPart>> GetPartsFromSectionAsync(int sectionId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromSection(ToIdSearchAndPageParams(sectionId, search, size, index), cancellationToken: cancellationToken), ToPart).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IProperty>> GetPartPropertiesAsync(int partId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartProperties(ToIdSearchAndPageParams(partId, search, size, index), cancellationToken: cancellationToken), ToProperty).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IModel>> GetSectionSpecificModelsAsync(int sectionId, int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetSectionSpecificModels(ToIdSearchAndPageParams(sectionId, search, size, index), cancellationToken: cancellationToken), ToModel).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<ISection> GetSectionAsync(int id, CancellationToken cancellationToken = default)
      => ToSection(await m_client.GetSectionAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<ISection>> GetSectionsAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllSections(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToSection).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<ISectionParts>> GetSectionPartChildren(int parentId, int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetSectionPartChildren(ToIdAndPageParams(parentId, size, index), cancellationToken: cancellationToken), ToSectionParts).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task<IPaging<IPropertyType>> GetPropertyTypesAsync(int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPropertyTypes(ToPageParams(size, index), cancellationToken: cancellationToken), ToPropertyType).ConfigureAwait(false);

    #endregion

    /// <inheritdoc />
    public void Dispose()
      => m_channel.Dispose();
  }
}
