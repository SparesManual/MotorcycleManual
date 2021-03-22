using System.Threading;
using System.Threading.Tasks;
using MRI.MVVM.Helpers;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for avatar views
  /// </summary>
  public class AvatarViewModel
    : BaseItemViewModel<string>
  {
    /// <inheritdoc />
    protected override async Task<string> GetItem(int id, CancellationToken cancellationToken = default)
    {
      return await Task.FromResult("");
    }
  }
}