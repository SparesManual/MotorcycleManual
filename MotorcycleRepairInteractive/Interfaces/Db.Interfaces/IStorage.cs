using System.Threading.Tasks;

namespace Db.Interfaces
{
  public interface IStorage
  {
    ValueTask SetItemAsync(string key, object value);
    ValueTask<T> GetItemAsync<T>(string key);
    ValueTask RemoveItemAsync(string key);
  }
}