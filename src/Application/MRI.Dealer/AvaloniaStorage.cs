using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Db.Interfaces;

namespace MRI.Dealer
{
  [DataContract]
  public class AvaloniaStorage
    : IStorage
  {
    [DataMember]
    public ConcurrentDictionary<string, object> Storage { get; set; } = new ConcurrentDictionary<string, object>();

    /// <inheritdoc />
    public ValueTask SetItemAsync(string key, object value)
    {
      Storage.AddOrUpdate(key, _ => value, (_, __) => value);

      return new ValueTask();
    }

    /// <inheritdoc />
    public ValueTask<T> GetItemAsync<T>(string key)
    {
      Storage.TryGetValue(key, out var val);

      return new ValueTask<T>((T)val!);
    }

    /// <inheritdoc />
    public ValueTask RemoveItemAsync(string key)
    {
      Storage.TryRemove(key, out _);

      return new ValueTask();
    }
  }
}