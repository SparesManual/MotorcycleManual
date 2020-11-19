using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Db.Core.Entities;
using Db.Core.Specifications;
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

    private static PartPropertyReply ToPropertyReply(Property? property)
      => new PartPropertyReply
      {
        Name = property?.PropertyName ?? string.Empty,
        Type = property?.PropertyType?.Name ?? string.Empty,
        TypeId = property?.PropertyTypeId ?? -1,
        Unit = property?.PropertyType?.Unit ?? string.Empty,
        Value = property?.PropertyValue ?? string.Empty
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

    private async Task GetAllAsync<T, TReply>(IGenericRepository<T> repository, ISpecification<T> specification, IAsyncStreamWriter<TReply> responseStream, Func<T?, TReply> converter, CancellationToken ct)
      where T : class, IEntity
      => await GetAllProcessorAsync(repository.GetAllAsync(specification), responseStream, converter, ct).ConfigureAwait(false);

    private async Task GetAllAsync<T, TReply>(IGenericRepository<T> repository, IAsyncStreamWriter<TReply> responseStream, Func<T?, TReply> converter, CancellationToken ct)
      where T : class, IEntity
      => await GetAllProcessorAsync(repository.GetAllAsync(), responseStream, converter, ct).ConfigureAwait(false);

    private async Task GetAllAsync<TParent, TChild, TReply>(IGenericRepository<TParent> repository, ISpecificationEx<TParent, TChild> specification, IAsyncStreamWriter<TReply> responseStream, Func<TChild?, TReply> converter, CancellationToken ct)
      where TParent : class, IEntity
      where TChild : class, IEntity
      => await GetAllProcessorAsync(repository.GetAllAsync(specification), responseStream, converter, ct).ConfigureAwait(false);

    private async Task GetAllProcessorAsync<T, TReply>(IAsyncEnumerable<T> items, IAsyncStreamWriter<TReply> responseStream, Func<T?, TReply> converter, CancellationToken ct)
      where T : class, IEntity
    {
      // For every item of requested type..
      await foreach (var item in items.WithCancellation(ct).ConfigureAwait(false))
        // write it to the response
        await responseStream.WriteAsync(converter(item)).ConfigureAwait(false);

      // If the operation was cancelled..
      if (ct.IsCancellationRequested)
        // log a warning
        m_logger.LogWarning($"Request for all entries of '{typeof(T).Name}' has been cancelled");
    }

    private static Metadata GeneratePagingMetadata(int total, int size, int index)
      => new Metadata
      {
        { "PageSize", size.ToString() },
        { "PageIndex", index.ToString() },
        { "TotalSize", total.ToString() }
      };

    #endregion

    #region API Calls

    /// <inheritdoc />
    public override async Task<BookReply> GetBook(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_bookRepository, ToBookReply);

    /// <inheritdoc />
    public override async Task GetBooks(PageParams pageParams, IServerStreamWriter<BookReply> responseStream, ServerCallContext context)
    {
      // TODO
      //await context.WriteResponseHeadersAsync(GeneratePagingMetadata()).ConfigureAwait(false);

      await GetAllAsync(m_bookRepository, responseStream, ToBookReply, context.CancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPartsFromSection(IdAndPageParams pageRequest, IServerStreamWriter<PartReply> responseStream, ServerCallContext context)
    {
      var specification = new SectionPartsSpec(pageRequest.Id, pageRequest.Size, pageRequest.Index);
      var count = await m_sectionPartsRepository.CountAsync(specification).ConfigureAwait(false);

      await context.WriteResponseHeadersAsync(GeneratePagingMetadata(count, pageRequest.Size, pageRequest.Index)).ConfigureAwait(false);

      await GetAllAsync(m_sectionPartsRepository, specification, responseStream, ToPartReply, context.CancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPartsFromBook(IdAndPageParams pageRequest, IServerStreamWriter<PartReply> responseStream, ServerCallContext context)
    {
      var specification = new BookPartsSpec(pageRequest.Id, pageRequest.Size, pageRequest.Index);
      var count = await m_sectionRepository.CountAsync(specification).ConfigureAwait(false);

      await context.WriteResponseHeadersAsync(GeneratePagingMetadata(count, pageRequest.Size, pageRequest.Index)).ConfigureAwait(false);

      await GetAllAsync(m_sectionRepository, specification, responseStream, ToPartReply, context.CancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<PartReply> GetPart(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_partRepository, ToPartReply);

    /// <inheritdoc />
    public override async Task GetPartProperties(IdAndPageParams pageRequest, IServerStreamWriter<PartPropertyReply> responseStream, ServerCallContext context)
    {
      var specification = new PartPropertiesSpec(pageRequest.Id, pageRequest.Size, pageRequest.Index);
      var count = await m_propertyRepository.CountAsync(specification).ConfigureAwait(false);

      await context.WriteResponseHeadersAsync(GeneratePagingMetadata(count, pageRequest.Size, pageRequest.Index)).ConfigureAwait(false);

      await GetAllAsync(m_propertyRepository, specification, responseStream, ToPropertyReply, context.CancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPropertyTypes(PageParams paging, IServerStreamWriter<PropertyTypeReply> responseStream, ServerCallContext context)
    {
      // TODO
      //await context.WriteResponseHeadersAsync(GeneratePagingMetadata()).ConfigureAwait(false);

      await GetAllAsync(m_propertyTypeRepository, responseStream, ToPropertyTypeReply, context.CancellationToken).ConfigureAwait(false);
    }

    #endregion
  }
}