using System.Threading;
using System.Threading.Tasks;
using GravatarHelper.NetStandard;
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
    protected override Task<string> GetItem(string id, CancellationToken cancellationToken = default)
    {
      var url = Gravatar.GetSecureGravatarImageUrl(id);

      return Task.FromResult(url);
    }
  }
}