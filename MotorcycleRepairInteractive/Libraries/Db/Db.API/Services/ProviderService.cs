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
    private readonly IGenericRepository<Make> m_makeRepository;
    private readonly IGenericRepository<BookMakes> m_bookMakesRepository;
    private readonly IGenericRepository<Model> m_modelRepository;
    private readonly IGenericRepository<MakeModels> m_makeModelsRepository;
    private readonly IGenericRepository<Section> m_sectionRepository;
    private readonly IGenericRepository<SectionParts> m_sectionPartsRepository;
    private readonly IGenericRepository<SectionPartParents> m_sectionPartParentsRepository;
    private readonly IGenericRepository<Part> m_partRepository;
    private readonly IGenericRepository<Property> m_propertyRepository;
    private readonly IGenericRepository<PropertyType> m_propertyTypeRepository;

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="logger">Injected logger instance</param>
    /// <param name="bookRepository">Injected book repository</param>
    /// <param name="makeModelsRepository"></param>
    /// <param name="sectionRepository">Injected section repository</param>
    /// <param name="sectionPartsRepository">Injected section parts repository</param>
    /// <param name="sectionPartParentsRepository">Injected section part parents repository</param>
    /// <param name="partRepository">Injected parts repository</param>
    /// <param name="propertyRepository">Injected property repository</param>
    /// <param name="propertyTypeRepository">Injected property type repository</param>
    /// <param name="makeRepository">Injected make repository</param>
    /// <param name="bookMakesRepository"></param>
    /// <param name="modelRepository">Injected model repository</param>
    public ProviderService(ILogger<ProviderService> logger,
      IGenericRepository<Book> bookRepository,
      IGenericRepository<Make> makeRepository,
      IGenericRepository<BookMakes> bookMakesRepository,
      IGenericRepository<Model> modelRepository,
      IGenericRepository<MakeModels> makeModelsRepository,
      IGenericRepository<Section> sectionRepository,
      IGenericRepository<SectionParts> sectionPartsRepository,
      IGenericRepository<SectionPartParents> sectionPartParentsRepository,
      IGenericRepository<Part> partRepository,
      IGenericRepository<Property> propertyRepository,
      IGenericRepository<PropertyType> propertyTypeRepository)
    {
      m_logger = logger;
      m_bookRepository = bookRepository;
      m_makeRepository = makeRepository;
      m_bookMakesRepository = bookMakesRepository;
      m_modelRepository = modelRepository;
      m_makeModelsRepository = makeModelsRepository;
      m_sectionRepository = sectionRepository;
      m_sectionPartsRepository = sectionPartsRepository;
      m_sectionPartParentsRepository = sectionPartParentsRepository;
      m_partRepository = partRepository;
      m_propertyRepository = propertyRepository;
      m_propertyTypeRepository = propertyTypeRepository;
    }

    #region Converters

    private static BookReply ToBookReply(Book? book)
      => new()
      {
        Id = book?.Id ?? -1,
        Title = book?.Title ?? string.Empty
      };

    private static MakeReply ToMakeReply(Make? make)
      => new()
      {
        Id = make?.Id ?? -1,
        Name = make?.Name ?? string.Empty,
        BookId = make?.ParentBook.BookId ?? -1
      };

    private static ModelReply ToModelReply(Model? model)
      => new()
      {
        Id = model?.Id ?? -1,
        Name = model?.Name ?? string.Empty,
      };

    private static PartReply ToPartReply(Part? part)
      => new()
      {
        Id = part?.Id ?? -1,
        PartNumber = part?.PartNumber ?? string.Empty,
        MakersPartNumber = part?.MakersPartNumber ?? string.Empty,
        Description = part?.Description ?? string.Empty,
      };

    private static PartPropertyReply ToPropertyReply(Property? property)
      => new()
      {
        Name = property?.PropertyName ?? string.Empty,
        Type = property?.PropertyType?.Name ?? string.Empty,
        TypeId = property?.PropertyTypeId ?? -1,
        Unit = property?.PropertyType?.Unit ?? string.Empty,
        Value = property?.PropertyValue ?? string.Empty
      };

    private static PropertyTypeReply ToPropertyTypeReply(PropertyType? propertyType)
      => new()
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

    private async Task GetAllExAsync<TParent, TChild, TReply>(IGenericRepository<TParent> repository, ISpecificationEx<TParent, TChild> specification, IAsyncStreamWriter<TReply> responseStream, Func<TChild?, TReply> converter, CancellationToken ct)
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
      => new()
      {
        { "PageSize", size.ToString() },
        { "PageIndex", index.ToString() },
        { "TotalSize", total.ToString() }
      };

    private static async Task ProcessPagedStream<TParent, TChild, TReply>(
      int size,
      int index,
      IGenericRepository<TParent> repository,
      ISpecificationEx<TParent, TChild> specification,
      IAsyncStreamWriter<TReply> responseStream,
      Func<IGenericRepository<TParent>, ISpecificationEx<TParent, TChild>, IAsyncStreamWriter<TReply>, Func<TChild?, TReply>, CancellationToken, Task> processor,
      Func<TChild?, TReply> converter,
      ServerCallContext context)
      where TParent : class, IEntity
      where TChild : class, IEntity
    {
      var count = await repository.CountAsync(specification).ConfigureAwait(false);

      await context.WriteResponseHeadersAsync(GeneratePagingMetadata(count, size, index)).ConfigureAwait(false);

      await processor(repository, specification, responseStream, converter, context.CancellationToken).ConfigureAwait(false);
    }

    private static async Task ProcessPagedStream<TChild, TReply>(
      int size,
      int index,
      IGenericRepository<TChild> repository,
      ISpecification<TChild> specification,
      IAsyncStreamWriter<TReply> responseStream,
      Func<IGenericRepository<TChild>, ISpecification<TChild>, IAsyncStreamWriter<TReply>, Func<TChild?, TReply>, CancellationToken, Task> processor,
      Func<TChild?, TReply> converter,
      ServerCallContext context)
      where TChild : class, IEntity
    {
      var count = await repository.CountAsync(specification).ConfigureAwait(false);

      await context.WriteResponseHeadersAsync(GeneratePagingMetadata(count, size, index)).ConfigureAwait(false);

      await processor(repository, specification, responseStream, converter, context.CancellationToken).ConfigureAwait(false);
    }

    #endregion

    #region API Calls

    /// <inheritdoc />
    public override async Task<BookReply> GetBook(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_bookRepository, ToBookReply).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task GetBooks(SearchAndPageParams pageParams, IServerStreamWriter<BookReply> responseStream, ServerCallContext context)
    {
      var specification = new BooksSpec(pageParams.Search, pageParams.Size, pageParams.Index);
      await ProcessPagedStream(pageParams.Size, pageParams.Index, m_bookRepository, specification, responseStream, GetAllAsync, ToBookReply, context).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<MakeReply> GetMake(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_makeRepository, ToMakeReply).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task<ModelReply> GetModel(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_modelRepository, ToModelReply).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task GetAllParts(SearchAndPageParams pageParams, IServerStreamWriter<PartReply> responseStream, ServerCallContext context)
    {
      var specification = new PartsSpec(pageParams.Search, pageParams.Size, pageParams.Index);
      await ProcessPagedStream(pageParams.Size, pageParams.Index, m_partRepository, specification, responseStream, GetAllAsync, ToPartReply, context).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPartsFromSection(IdSearchAndPageParams pageRequest, IServerStreamWriter<PartReply> responseStream, ServerCallContext context)
    {
      var specification = new SectionPartsSpec(pageRequest.Id, pageRequest.Search, pageRequest.Size, pageRequest.Index);
      await ProcessPagedStream(pageRequest.Size, pageRequest.Index, m_sectionPartsRepository, specification, responseStream, GetAllExAsync, ToPartReply, context).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPartsFromBook(IdSearchAndPageParams pageRequest, IServerStreamWriter<PartReply> responseStream, ServerCallContext context)
    {
      var specification = new BookPartsSpec(pageRequest.Id, pageRequest.Search, pageRequest.Size, pageRequest.Index);
      await ProcessPagedStream(pageRequest.Size, pageRequest.Index, m_sectionRepository, specification, responseStream, GetAllExAsync, ToPartReply, context).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task<PartReply> GetPart(IdRequest request, ServerCallContext context)
      => await GetById(request.Id, m_partRepository, ToPartReply).ConfigureAwait(false);

    /// <inheritdoc />
    public override async Task GetPartProperties(IdSearchAndPageParams pageRequest, IServerStreamWriter<PartPropertyReply> responseStream, ServerCallContext context)
    {
      var specification = new PartPropertiesSpec(pageRequest.Id, pageRequest.Search, pageRequest.Size, pageRequest.Index);
      await ProcessPagedStream(pageRequest.Size, pageRequest.Index, m_propertyRepository, specification, responseStream, GetAllAsync, ToPropertyReply, context).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async Task GetPropertyTypes(PageParams paging, IServerStreamWriter<PropertyTypeReply> responseStream, ServerCallContext context)
    {
      var specification = new PropertyTypesSpec(paging.Size, paging.Index);
      await ProcessPagedStream(paging.Size, paging.Index, m_propertyTypeRepository, specification, responseStream, GetAllAsync, ToPropertyTypeReply, context).ConfigureAwait(false);
    }

    #endregion
  }
}