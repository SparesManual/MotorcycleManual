using System.Collections.Generic;
using System.Linq;

namespace MRI.Helpers
{
  public static class LinqHelpers
  {
    public static TKey GetKey<TKey, TValue>(this IGrouping<TKey, TValue> grouping)
      => grouping.Key;

    public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable)
      => new LinkedList<T>(enumerable);

    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
      => enumerable.ToLinkedList();
  }
}
