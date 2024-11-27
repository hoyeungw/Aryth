using System;

namespace Aryth.Bounds {
  internal static class CoreUtil {
    internal static (T min, T max) InitBound<T>(T value) => (value, value);
    internal static (T min, T max) AssortBound<T>((T min, T max) bound, T value) where T : IComparable<T> {
      if (value.CompareTo(bound.max) > 0) { bound.max = value; }
      else if (value.CompareTo(bound.min) < 0) { bound.min = value; }
      return bound;
    }
  }
}