using System.Threading;
using System.Threading.Tasks;
using Media.Interfaces;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for avatar views
  /// </summary>
  public class AvatarViewModel
    : BaseItemViewModel<string>, IAvatarViewModel
  {
    private readonly IMediaAPI m_mediaAPI;

    /// <summary>
    /// Default constructor
    /// </summary>
    public AvatarViewModel(IMediaAPI mediaAPI)
    {
      m_mediaAPI = mediaAPI;
    }

    /// <inheritdoc />
    protected override async Task<string> GetItem(string id, CancellationToken cancellationToken = default)
    {
      var url = await m_mediaAPI.GetUserProfileAsync(id).ConfigureAwait(false);
      return url;
    }
  }
}