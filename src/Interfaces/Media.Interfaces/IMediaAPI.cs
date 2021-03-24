using System.Threading.Tasks;

namespace Media.Interfaces
{
  /// <summary>
  /// Interface for media API providers
  /// </summary>
  public interface IMediaAPI
  {
    /// <summary>
    /// Retrieves the profile picture url for a given users email
    /// </summary>
    /// <param name="email">User email</param>
    /// <returns>Profile url</returns>
    ValueTask<string> GetUserProfileAsync(string email);

    /// <summary>
    /// Retrieves the image url for a given section
    /// </summary>
    /// <param name="id">Section id</param>
    /// <returns>Section image url</returns>
    ValueTask<string> GetSectionImageAsync(int id);
  }
}