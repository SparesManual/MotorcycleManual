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
      foreach (var item in result.Items)
        Autocomplete.Add($"{item.PartNumber} {item.Description}");
    }

    /// <inheritdoc />
    protected override Task<IPaging<IPart>> GetItemsAsync(int pageSize, int pageIndex, string? search, CancellationToken cancellationToken = default)
      => m_provider.GetPartsAsync(pageSize, pageIndex, search, cancellationToken);

    #endregion
  }
}
