using System.Configuration;
using System.Threading.Tasks;
using Db.Interfaces;
using MRI.Dealer.Properties;

namespace MRI.Dealer
{
  public class AvaloniaStorage
    : IStorage
  {
    /// <inheritdoc />
    public ValueTask SetItemAsync(string key, object value)
    {
      Settings.Default.Properties.Add(new SettingsProperty(key) { PropertyType = value.GetType(), DefaultValue = value, Name = key});
      Settings.Default.Save();

      return new ValueTask();
    }

    /// <inheritdoc />
    public ValueTask<T> GetItemAsync<T>(string key)
      => new((T)Settings.Default.Properties[key].DefaultValue);

    /// <inheritdoc />
    public ValueTask RemoveItemAsync(string key)
    {
      Settings.Default.Properties.Remove(key);
      Settings.Default.Save();

      return new ValueTask();
    }
  }
}