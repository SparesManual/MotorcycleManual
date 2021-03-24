using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Media.Interfaces;

namespace MRI.Media
{
  /// <summary>
  /// Media providing API client
  /// </summary>
  public class MediaAPI
    : IMediaAPI
  {
    /// <inheritdoc />
    public ValueTask<string> GetUserProfileAsync(string email)
    {
      if (string.IsNullOrEmpty(email))
        return new ValueTask<string>(string.Empty);

      using var md5 = MD5.Create();
      var inputBytes = Encoding.ASCII.GetBytes(email);
      var hashBytes = md5.ComputeHash(inputBytes);

      var builder = new StringBuilder();
      foreach (var b in hashBytes)
        builder.Append(b.ToString("X2"));

      return new ValueTask<string>($"https://www.gravatar.com/avatar/{builder.ToString().ToLower()}?s={256}");
    }

    /// <inheritdoc />
    public ValueTask<string> GetSectionImageAsync(int id) => throw new NotImplementedException();
  }
}