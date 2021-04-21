using System;
using System.Text;
using System.Threading.Tasks;
using Media.Interfaces;
using MRI.Helpers;

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

      var md5 = new MD5(Encoding.ASCII.GetBytes(email));
      return new ValueTask<string>($"https://www.gravatar.com/avatar/{md5.HexDigest.ToLower()}?s={256}&d=mm&r=g");
    }

    /// <inheritdoc />
    public ValueTask<string> GetSectionImageAsync(int id) => throw new NotImplementedException();
  }
}