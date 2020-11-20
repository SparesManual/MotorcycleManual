using System;
using System.Threading;
using System.Threading.Tasks;
using Db.API;
using Db.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;

namespace MRI.Db
{
  /// <summary>
  /// Provider of API method calls to the Database
  /// </summary>
  public sealed class APIProvider
    : IDisposable
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
      => new IdRequest
      {
        Id = id
      };

    private static PageParams ToPageParams(int size, int index)
      => new PageParams
      {
        Index = index,
        Size = size
      };

    private static IdAndPageParams ToIdAndPageParams(int id, int size, int index)
      => new IdAndPageParams
      {
        Id = id,
        Size = size,
        Index = index
      };

    private async Task<IPaging<T>> GetPagingAsync<T>(Func<Provider.ProviderClient, AsyncServerStreamingCall<T>> extractor)
    {
      var result = extractor(m_client);
      var headers = await result.ResponseHeadersAsync.ConfigureAwait(false);
      var (total, pageSize, pageIndex) = RetrievePaging(headers);

      return new Paging<T>(result.ResponseStream, total, pageSize, pageIndex);
    }

    private static (int total, int size, int index) RetrievePaging(Metadata metadata)
      => (int.Parse(metadata.GetValue("totalsize")), int.Parse(metadata.GetValue("pagesize")), int.Parse(metadata.GetValue("pageindex")));

    #endregion

    #region API

    /// <summary>
    /// Get a book based on the given <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the book</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Retrieved book</returns>
    public async Task<BookReply> GetBookAsync(int id, CancellationToken cancellationToken = default)
      => await m_client.GetBookAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);

    /// <summary>
    /// Get all books
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of books</returns>
    public async Task<IPaging<BookReply>> GetBooksAsync(int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetBooks(ToPageParams(size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    /// <summary>
    /// Get a part based on the given <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id of the part</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Retrieved part</returns>
    public async Task<PartReply> GetPartAsync(int id, CancellationToken cancellationToken = default)
      => await m_client.GetPartAsync(ToIdRequest(id), cancellationToken: cancellationToken).ResponseAsync.ConfigureAwait(false);

    /// <summary>
    /// Get all parts
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    public async Task<IPaging<PartReply>> GetPartsAsync(int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetAllParts(ToPageParams(size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    /// <summary>
    /// Get all parts belonging to a book with the given <paramref name="bookId"/>
    /// </summary>
    /// <param name="bookId">Id of the parent book</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    public async Task<IPaging<PartReply>> GetPartsFromBookAsync(int bookId, int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromBook(ToIdAndPageParams(bookId, size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    /// <summary>
    /// Get all parts belonging to a section with the given <paramref name="sectionId"/>
    /// </summary>
    /// <param name="sectionId">Id of the parent section</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of parts</returns>
    public async Task<IPaging<PartReply>> GetPartsFromSectionAsync(int sectionId, int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartsFromSection(ToIdAndPageParams(sectionId, size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    /// <summary>
    /// Get all properties for a part with the given <paramref name="partId"/>
    /// </summary>
    /// <param name="partId">Id of the part</param>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of properties</returns>
    public async Task<IPaging<PartPropertyReply>> GetPartPropertiesAsync(int partId, int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPartProperties(ToIdAndPageParams(partId, size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    /// <summary>
    /// Get all property types
    /// </summary>
    /// <param name="size">Page size</param>
    /// <param name="index">Page index</param>
    /// <param name="cancellationToken">Cancellation</param>
    /// <returns>Paging batch of property types</returns>
    public async Task<IPaging<PropertyTypeReply>> GetPropertyTypesAsync(int size, int index, CancellationToken cancellationToken = default)
      => await GetPagingAsync(client => client.GetPropertyTypes(ToPageParams(size, index), cancellationToken: cancellationToken)).ConfigureAwait(false);

    #endregion

    /// <inheritdoc />
    public void Dispose()
      => m_channel.Dispose();
  }
}
