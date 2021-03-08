using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.Helpers;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying book models
  /// </summary>
  public class BookModelsViewModel
    : BaseItemViewModel<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>>, IBookModelsViewModel
  {
    /// <inheritdoc />
    public BookModelsViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override async Task<IReadOnlyDictionary<string, IReadOnlyCollection<IModel>>> GetItem(int id, CancellationToken cancellationToken = default)
    {
      // Get the models
      var models = await m_provider
        // Query the models from a given book
        .GetBookModelsAsync(id, cancellationToken)
        // Materialize the result
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

      // Return the result
      return models
        // Group the models by their names
        .GroupBy(model => model.Name)
        // Materialize the items into a map of model name to concrete models
        .ToDictionary(group => group.Key, group => group.ToReadOnlyCollection());
    }
  }
}