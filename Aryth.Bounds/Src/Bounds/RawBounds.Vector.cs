using System;
using System.Collections.Generic;
using Veho.Sequence;

namespace Aryth.Bounds {
  public static partial class RawBounds {
    private static (T min, T max) InitBound<T>(T value) => (value, value);
    private static (T min, T max) AssortBound<T>((T min, T max) bound, T value) where T : IComparable<T> {
      if (value.CompareTo(bound.max) > 0) { bound.max = value; } else if (value.CompareTo(bound.min) < 0) { bound.min = value; }
      return bound;
    }
    public static (T min, T max)? Bound<T>(this IReadOnlyList<T> vec) where T : IComparable<T> => vec.Count == 0
      ? ((T min, T max)?)null
      : vec.Fold(AssortBound, InitBound);
  }
}