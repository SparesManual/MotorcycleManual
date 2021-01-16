using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  public class BookModelsViewModel
    : BaseItemViewModel<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>>, IBookModelsViewModel
  {
    /// <inheritdoc />
    public int BookId { get; }

    /// <inheritdoc />
    public BookModelsViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override async Task<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>> GetItem(int id, CancellationToken cancellationToken = default)
    {
      var models = await m_provider
        .GetBookModelsAsync(id, cancellationToken)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

      return models
        .GroupBy(model => model.Name)
        .ToDictionary(group => group.Key, group => new LinkedList<IModel>(group) as IReadOnlyCollection<IModel>);
    }
  }
}