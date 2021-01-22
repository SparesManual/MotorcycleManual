using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for paging all parts
  /// </summary>
  public class PartsAllViewModel
    : BasePagedViewModel<IPart>, IAllPartsViewModel
  {
    /// <inheritdoc />
    public ObservableCollection<string> Autocomplete { get; } = new();

    /// <inheritdoc />
    public PartsAllViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    #region Methods

    /// <inheritdoc />
    public async Task UpdateAutocomplete(string? search)
    {
      Autocomplete.Clear();

      var result = await m_provider.GetPartsAsync(5, 1, search).ConfigureAwait(true);
      await foreach (var item in result.ReadAll().ConfigureAwait(true))
        Autocomplete.Add($"{item.PartNumber} {item.Description}");
    }

    /// <inheritdoc />
    protected override async Task<IPaging<IPart>> GetItems(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => await m_provider.GetPartsAsync(pageSize, pageIndex, search, cancellationToken).ConfigureAwait(false);

    #endregion
  }
}
