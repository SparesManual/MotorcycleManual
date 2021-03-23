using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace Media.API.Services
{
  /// <summary>
  /// Media API service
  /// </summary>
  // ReSharper disable once ClassNeverInstantiated.Global
  public class MediaService
    : MediaProvider.MediaProviderBase
  {
    /// <inheritdoc />
    public override Task<StringMessage> GetUserProfile(StringMessage request, ServerCallContext context)
    {
      if (string.IsNullOrEmpty(request.Content))
        return Task.FromResult(new StringMessage
        {
          Content = string.Empty
        });

      using var md5 = MD5.Create();
      var inputBytes = Encoding.ASCII.GetBytes(request.Content);
      var hashBytes = md5.ComputeHash(inputBytes);

      var sb = new StringBuilder();
      foreach (var b in hashBytes)
        sb.Append(b.ToString("X2"));

      return Task.FromResult(new StringMessage
      {
        Content = $"https://www.gravatar.com/avatar/{sb.ToString().ToLower()}?s={256}"
      });
    }
  }
}