using System;
using System.Threading;
using System.Threading.Tasks;
using Email.Interfaces;

namespace MRI.Email
{
  /// <summary>
  /// FluentEmail API mailing Client
  /// </summary>
  public class EmailClient
    : IAPIMail
  {
    #region API Methods

    /// <inheritdoc />
    public ValueTask SendRegistrationConfirmationAsync(string email, string code, CancellationToken cancellationToken = default)
      => throw new NotImplementedException();

    /// <inheritdoc />
    public ValueTask SendRecoveryCodeAsync(string email, string code, CancellationToken cancellationToken = default)
      => throw new NotImplementedException();

    #endregion
  }
}