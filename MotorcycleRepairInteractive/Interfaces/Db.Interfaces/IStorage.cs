using System.Threading.Tasks;

namespace Db.Interfaces
{
  /// <summary>
  /// Interface representing a local storage instance
  /// </summary>
  public interface IStorage
  {
    /// <summary>
    /// Saves a given <paramref name="value"/> under a given <paramref name="key"/>
    /// </summary>
    /// <param name="key">Key to save under</param>
    /// <param name="value">Value to save</param>
    ValueTask SetItemAsync(string key, object value);

    /// <summary>
    /// Retrieves a value based on the given <paramref name="key"/>
    /// </summary>
    /// <param name="key">Key of the value</param>
    /// <typeparam name="T">Type of the value</typeparam>
    /// <returns>Retrieved value</returns>
    ValueTask<T> GetItemAsync<T>(string key);

    /// <summary>
    /// Removes a value based on the given <paramref name="key"/>
    /// </summary>
    /// <param name="key">Key of the value</param>
    ValueTask RemoveItemAsync(string key);
  }
}