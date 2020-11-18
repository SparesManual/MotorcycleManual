using System;
using System.Threading;
using System.Threading.Tasks;
using Db.Core.Entities;
using Db.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Db.API
{
  /// <summary>
  /// Database provider API service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class ProviderService
    : Provider.ProviderBase
  {
    #region Fields

    private readonly ILogger<ProviderService> m_logger;
    private readonly IGenericRepository<Book> m_bookRepository;
    private readonly IGenericRepository<Section> m_sectionRepository;
    private readonly IGenericRepository<SectionParts> m_sectionPartsRepository;
    private readonly IGenericRepository<Part> m_partRepository;
    private readonly IGenericRepository<Property> m_propertyRepository;
    private readonly IGenericRepository<PropertyType> m_propertyTypeRepository;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="logger">Injected logger instance</param>
    /// <param name="bookRepository">Injected book repository</param>
    /// <param name="sectionRepository">Injected section repository</param>
    /// <param name="sectionPartsRepository">Injected section parts repository</param>
    /// <param name="partRepository">Injected parts repository</param>
    /// <param name="propertyRepository">Injected property repository</param>
    /// <param name="propertyTypeRepository">Injected property type repository</param>
    public ProviderService(ILogger<ProviderService> logger,
      IGenericRepository<Book> bookRepository,
      IGenericRepository<Section> sectionRepository,
      IGenericRepository<SectionParts> sectionPartsRepository,
      IGenericRepository<Part> partRepository,
      IGenericRepository<Property> propertyRepository,
      IGenericRepository<PropertyType> propertyTypeRepository)
    {
      m_logger = logger;
      m_bookRepository = bookRepository;
      m_sectionRepository = sectionRepository;
      m_sectionPartsRepository = sectionPartsRepository;
      m_partRepository = partRepository;
      m_propertyRepository = propertyRepository;
      m_propertyTypeRepository = propertyTypeRepository;
    }

    #region Converters

    private static BookReply ToBookReply(Book? book)
      => new BookReply
      {
        Id = book?.Id ?? -1,
        Title = book?.Title ?? string.Empty
      };

    private static PartReply ToPartReply(Part? part)
      => new PartReply
      {
        Id = part?.Id ?? -1,
        PartNumber = part?.PartNumber ?? string.Empty,
        MakersPartNumber = part?.MakersPartNumber ?? string.Empty,
        Description = part?.Description ?? string.Empty,
        MakersDescription = part?.MakersDescription ?? string.Empty
      };

    private static PropertyTypeReply ToPropertyTypeReply(PropertyType? propertyType)
      => new PropertyTypeReply
      {
        Id = propertyType?.Id ?? -1,
        Name = propertyType?.Name ?? string.Empty,
        Unit = propertyType?.Unit ?? string.Empty
      };

    #endregion

    #region Helpers

    private async Task<TReply> GetById<T, TReply>(int id, IGenericRepository<T> repository, Func<T?, TReply> converter)
      where T : class, IEntity
    {
      // Get the requested item by its id
      var item = await repository.GetByIdAsync(id).ConfigureAwait(false);

      // If no item was found..
      if (item is null)
        // log a warning
        m_logger.LogWarning($"Request for unknown '{typeof(T).Name}' with id '{id}'");

      // Return the item converted to its equivalent proto-reply
      return converter(item);
    }

    private async Task GetAllAsync<T, TReply>(IGenericRepository<T> repository, IAsyncStreamWriter<TReply> responseStream, Func<T?, TReply> converter, CancellationToken ct)
      where T : class, IEntity
    {
      // For every item of requested type..
      await foreach (var item in repository.GetAllAsync().WithCancellation(ct).ConfigureAwait(false))
        // write it to the response
        await responseStream.WriteAsync(converter(item)).ConfigureAwait(false);

      // If the operation was cancelled..
      if (ct.IsCancellationRequested)
        // log a warning
        m_logger.LogWarning($"Request for all entries of '{typeof(T).Name}' has been cancelled");
    }

    private static Metadata GeneratePagingMetadata()
      => new Metadata
      {
        { "Page", 0.ToString() }
      };

    #endregion

    #region API Calls

    /// <inheritdoc />
    public override async Task<BookReply> GetBook(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_bookRepository, ToBookReply);

    /// <inheritdoc />
    public override async Task GetBooks(Empty request, IServerStreamWriter<BookReply> responseStream, ServerCallContext context)
    {
      await context.WriteResponseHeadersAsync(GeneratePagingMetadata()).ConfigureAwait(false);

      await GetAllAsync(m_bookRepository, responseStream, ToBookReply, context.CancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<PartReply> GetPart(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_partRepository, ToPartReply);

    /// <inheritdoc />
    public override async Task GetPropertyTypes(Empty request, IServerStreamWriter<PropertyTypeReply> responseStream, ServerCallContext context)
    {
      await context.WriteResponseHeadersAsync(GeneratePagingMetadata()).ConfigureAwait(false);

      await GetAllAsync(m_propertyTypeRepository, responseStream, ToPropertyTypeReply, context.CancellationToken).ConfigureAwait(false);
    }

    #endregion
  }
}