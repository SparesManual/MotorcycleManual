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
    /// <inheritdoc />
    public ModelViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override async Task<IModel> GetItem(int id, CancellationToken cancellationToken = default)
      => await m_provider.GetModelAsync(id, cancellationToken).ConfigureAwait(false);
  }
}