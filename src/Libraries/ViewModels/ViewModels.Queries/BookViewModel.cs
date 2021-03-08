using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying <see cref="IBook"/> information
  /// </summary>
  public class BookViewModel
    : BaseItemViewModel<IBook>, IBookViewModel
  {
    /// <inheritdoc />
    public BookViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override async Task<IBook> GetItem(int id, CancellationToken cancellationToken = default)
      => await m_provider.GetBookAsync(id, cancellationToken).ConfigureAwait(false);
  }
}