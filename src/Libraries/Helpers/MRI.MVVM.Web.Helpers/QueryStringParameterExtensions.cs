using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace MRI.MVVM.Web.Helpers
{
  /// <summary>
  /// Helper class for working with query string parameters
  /// </summary>
  public static class QueryStringParameterExtensions
  {
    /// <summary>
    /// Apply the values from the query string to the current component
    /// </summary>
    /// <param name="component">Parent component</param>
    /// <param name="navigationManager">Navigation manager instance</param>
    /// <typeparam name="T">Component type</typeparam>
    /// <exception cref="InvalidOperationException">If the current URL is invalid</exception>
    public static void SetParametersFromQueryString<T>(this T component, NavigationManager navigationManager)
      where T : ComponentBase
    {
      if (!Uri.TryCreate(navigationManager.Uri, UriKind.RelativeOrAbsolute, out var uri))
        throw new InvalidOperationException("The current url is not a valid URI. Url: " + navigationManager.Uri);

      // Parse the query string
      Dictionary<string, StringValues> queryString = QueryHelpers.ParseQuery(uri.Query);

      // Enumerate all properties of the component
      foreach (var property in GetProperties<T>())
      {
        // Get the name of the parameter to read from the query string
        var parameterName = GetQueryStringParameterName(property);
        if (parameterName is null)
          continue; // The property is not decorated by [QueryStringParameterAttribute]

        if (!queryString.TryGetValue(parameterName, out var value))
          continue;

        // Convert the value from string to the actual property type
        var convertedValue = ConvertValue(value, property.PropertyType);
        property.SetValue(component, convertedValue);
      }
    }

    /// <summary>
    /// Apply the values from the component to the query string
    /// </summary>
    /// <param name="component">Parent component</param>
    /// <param name="navigationManager">Navigation manager instance</param>
    /// <typeparam name="T">Component type</typeparam>
    /// <exception cref="InvalidOperationException">If the current URL is invalid</exception>
    public static void UpdateQueryString<T>(this T component, NavigationManager navigationManager)
      where T : ComponentBase
    {
      if (!Uri.TryCreate(navigationManager.Uri, UriKind.RelativeOrAbsolute, out var uri))
        throw new InvalidOperationException("The current url is not a valid URI. Url: " + navigationManager.Uri);

      // Fill the dictionary with the parameters of the component
      var parameters = QueryHelpers.ParseQuery(uri.Query);
      foreach (var property in GetProperties<T>())
      {
        var parameterName = GetQueryStringParameterName(property);
        if (parameterName == null)
          continue;

        var value = property.GetValue(component);
        if (value is null)
          parameters.Remove(parameterName);
        else
        {
          var convertedValue = ConvertToString(value);
          parameters[parameterName] = convertedValue;
        }
      }

      // Compute the new URL
      var newUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);
      foreach (var (key, stringValues) in parameters)
        newUri = stringValues.Aggregate(newUri, (current, value) => QueryHelpers.AddQueryString(current, key, value));

      navigationManager.NavigateTo(newUri, true);
    }

    private static object ConvertValue(StringValues value, Type type)
      => Convert.ChangeType(value[0], type, CultureInfo.InvariantCulture);

    private static string? ConvertToString(object value)
      => Convert.ToString(value, CultureInfo.InvariantCulture);

    private static IEnumerable<PropertyInfo> GetProperties<T>()
      => typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    private static string? GetQueryStringParameterName(MemberInfo property)
    {
      var attribute = property.GetCustomAttribute<QueryStringParameterAttribute>();

      return attribute?.Name;
    }
  }
}