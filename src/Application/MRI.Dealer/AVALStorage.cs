using System.Threading.Tasks;
using Db.Interfaces;

namespace MRI.Dealer
{
  public class AVALStorage
    : IStorage
  {
    /// <inheritdoc />
    public ValueTask SetItemAsync(string key, object value)
    {
      // JsonFormatter.Settings.Default.Properties.Add(new SettingsProperty(key) { PropertyType = value.GetType(), DefaultValue = value, Name = key});
      // JsonFormatter.Settings.Default.Save();

      return new ValueTask();
    }

    /// <inheritdoc />
    public ValueTask<T> GetItemAsync<T>(string key)
      => new(default(T));

    /// <inheritdoc />
    public ValueTask RemoveItemAsync(string key)
    {
      // JsonFormatter.Settings.Default.Properties.Remove(key);
      // JsonFormatter.Settings.Default.Save();

      return new ValueTask();
    }
  }
}