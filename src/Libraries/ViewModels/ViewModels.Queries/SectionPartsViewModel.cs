using System.Threading;
using System.Threading.Tasks;
using Db.Interfaces;
using Models.Interfaces.Entities;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Queries;

namespace ViewModels.Queries
{
  /// <summary>
  /// View model for displaying section parts
  /// </summary>
  public class SectionPartsViewModel
    : BasePagedViewModel<ISectionPart>, ISectionPartsViewModel
  {
    /// <inheritdoc />
    public int? SectionId { get; set; }

    /// <inheritdoc />
    public SectionPartsViewModel(IAPIProvider provider)
      : base(provider)
    {
    }

    /// <inheritdoc />
    protected override ValueTask<IPaging<ISectionPart>> GetItemsAsync(int pageSize, int pageIndex, string? search,
      CancellationToken cancellationToken = default)
      => m_provider.GetPartsFromSectionAsync(SectionId ?? 0, pageSize, pageIndex, cancellationToken: cancellationToken);
  }
}