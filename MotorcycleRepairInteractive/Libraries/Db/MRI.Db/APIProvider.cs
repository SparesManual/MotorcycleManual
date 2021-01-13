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

      async IAsyncEnumerable<TReplyModel> ConvertStream([EnumeratorCancellation] CancellationToken cancellationToken)
      {
        await foreach (var item in result.ResponseStream.ReadAllAsync(cancellationToken).WithCancellation(cancellationToken).ConfigureAwait(false))
          yield return converter(item);
      }

      return new Paging<TReplyModel>(ConvertStream, total, pageSize, pageIndex);
    }

    private static (int total, int size, int index) RetrievePaging(Metadata metadata)
      => (int.Parse(metadata.GetValue("totalsize")), int.Parse(metadata.GetValue("pagesize")), int.Parse(metadata.GetValue("pageindex")));

    private static IBook ToBook(BookReply reply)
      => new BookModel(reply.Id, reply.Title);

    private static IPart ToPart(PartReply reply)
      => new PartModel(reply.Id, reply.PartNumber, reply.MakersPartNumber, reply.Description);

    private static IProperty ToProperty(PartPropertyReply reply)
      => new PropertyModel(reply.TypeId, reply.Name, reply.Value, reply.Type, reply.Unit);

    private static IPropertyType ToPropertyType(PropertyTypeReply reply)
      => new PropertyTypeModel(reply.Id, reply.Name, reply.Unit);

    private static ISection ToSection(SectionReply reply)
      => new SectionModel(reply.Id, reply.BookId, reply.Header, reply.StartPage, reply.EndPage, reply.FigureNumber, reply.FigureDescription);

    private static ISectionParts ToSectionParts(SectionPartReply reply)
      => new SectionPartModel(reply.Id, reply.PartId, reply.PageNumber, reply.Remarks, reply.AdditionalInfo, reply.Quantity);

    #endregion

    #region API

    /// <inheritdoc />
    public async Task<IBook> GetBookAsync(int id, CancellationToken cancellationToken = default)
      => ToBook(await m_client.GetBookAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false));

    /// <inheritdoc />
    public async Task<IPaging<IBook>> GetBooksAsync(int size, int index, string? search = default, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetBooks(ToSearchAndPageParams(search, size, index), cancellationToken: cancellationToken), ToBook).ConfigureAwait(false);

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
