using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying a model
  /// </summary>
  public class ModelViewModel
    : BaseItemViewModel<IModel>, IModelViewModel
  {
    private readonly IAPIProvider m_provider;

    /// <inheritdoc />
    public ModelViewModel(IAPIProvider provider)
    {
      m_provider = provider;
    }

    /// <inheritdoc />
    protected override async Task<IModel> GetItem(string id, CancellationToken cancellationToken = default)
      => await m_provider.GetModelAsync(IdToInt(id), cancellationToken).ConfigureAwait(false);
  }
}