using System.Threading.Tasks;
using Blazored.LocalStorage;
using Db.Interfaces;

namespace MRI.Client
{
  public class StorageProvider
    : IStorage
  {
    private readonly ILocalStorageService m_storageService;

    public StorageProvider(ILocalStorageService storageService)
    {
      m_storageService = storageService;
    }

    /// <inheritdoc />
    public ValueTask SetItemAsync(string key, object value)
      => m_storageService.SetItemAsync(key, value);

    /// <inheritdoc />
    public ValueTask<T> GetItemAsync<T>(string key)
      => m_storageService.GetItemAsync<T>(key);

    /// <inheritdoc />
    public ValueTask RemoveItemAsync(string key)
      => m_storageService.RemoveItemAsync(key);
  }
}
