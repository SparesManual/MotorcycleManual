using System;
using System.Collections.Generic;
using System.Linq;

namespace MRI.Helpers
{
  /// <summary>
  /// LINQ helpers class
  /// </summary>
  public static class LinqHelpers
  {
    /// <summary>
    /// Gets the key of the given <paramref name="grouping"/>
    /// </summary>
    /// <typeparam name="TKey">Grouping keys type</typeparam>
    /// <typeparam name="TValue">Grouping values type</typeparam>
    /// <param name="grouping">Grouping to process</param>
    /// <returns>Extracted key from the <paramref name="grouping"/></returns>
    public static TKey GetKey<TKey, TValue>(this IGrouping<TKey, TValue> grouping)
      => grouping.Key;

    /// <summary>
    /// Adds a range of nodes to the end of the <see cref="LinkedList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the nudes</typeparam>
    /// <param name="subject">List to add items to</param>
    /// <param name="input">Items to add</param>
    public static void AddRange<T>(this LinkedList<T> subject, IEnumerable<T>? input)
    {
      if (subject is null)
        throw new ArgumentNullException(nameof(subject));

      foreach (var item in input ?? Enumerable.Empty<T>())
        subject.AddLast(item);
    }

    /// <summary>
    /// Returns distinct elements from a sequence by using the default equality comparer to compare values by a given key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TKey">The type of keys by which the elements from <paramref name="source"/> will be compared by.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="keySelector">A retrieve function to select each sequence items given key.</param>
    /// <returns>An System.Collections.Generic.IEnumerable`1 that contains distinct elements from the source sequence.</returns>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
      if (source is null)
        throw new ArgumentNullException(nameof(source));
      if (keySelector is null)
        throw new ArgumentNullException(nameof(keySelector));

      var seenKeys = new HashSet<TKey>();
      foreach (var element in source)
        if (seenKeys.Add(keySelector(element)))
          yield return element;
    }

    /// <summary>
    /// Create a read only collection from the <paramref name="input"/>
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <typeparam name="T">Input collection items type</typeparam>
    /// <returns>Read only collection</returns>
    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> input)
      => input.ToLinkedList();

    /// <summary>
    /// Create a linked list from the <paramref name="input"/>
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <typeparam name="T">Input collection items type</typeparam>
    /// <returns>Linked list</returns>
    public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> input)
      => new(input);

    /// <summary>
    /// Filters out null types
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <param name="predicate">Extracts data from the <paramref name="input"/> collection</param>
    /// <typeparam name="T">Input collection items type</typeparam>
    /// <typeparam name="TProp">Type of the extracted data from the <paramref name="input"/> collection</typeparam>
    /// <returns>Filtered enumeration</returns>
    public static IEnumerable<T> WhereNotNull<T, TProp>(this IEnumerable<T> input, Func<T, TProp?> predicate)
      where TProp : class
      => input.Where(x => predicate(x) != null)!;

    /// <summary>
    /// Filters out null types
    /// </summary>
    /// <param name="input">Input to process</param>
    /// <typeparam name="T">Input collection items type</typeparam>
    /// <returns>Filtered enumeration</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> input)
      where T : class
      => input.Where(x => x != null)!;
  }
}
