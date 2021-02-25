using System.Threading.Tasks;
using Db.Interfaces;
using MRI.MVVM.Helpers;
using ViewModels.Interfaces.Auth.Validators;
using ViewModels.Interfaces.Auth.ViewModels;

namespace ViewModels.Auth
{
  /// <summary>
  /// View model for logout views
  /// </summary>
  public class LogoutViewModel
    : BaseFormViewModel<ILogoutViewModelValidator>, ILogoutViewModel
  {
    private readonly IAPIAuth m_authProvider;

    /// <inheritdoc />
    public LogoutViewModel(ILogoutViewModelValidator validator, IAPIAuth authProvider)
      : base(validator)
    {
      m_authProvider = authProvider;
    }

    /// <inheritdoc />
    public async ValueTask<bool> LogoutUser()
      => await m_authProvider.LogoutUserAsync().ConfigureAwait(false);
  }
}