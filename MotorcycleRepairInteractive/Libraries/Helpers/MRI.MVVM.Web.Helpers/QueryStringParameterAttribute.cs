using System;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Attribute for complex query string parameters
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  // ReSharper disable once ClassNeverInstantiated.Global
  public sealed class QueryStringParameterAttribute
    : Attribute
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public QueryStringParameterAttribute()
    {
    }

    /// <summary>
    /// Parametrized constructor
    /// </summary>
    /// <param name="name">Parameter name</param>
    public QueryStringParameterAttribute(string name)
      => Name = name;

    /// <summary>
    /// Name of the query string parameter
    /// </summary>
    public string Name { get; } = string.Empty;
  }
}