using System;
using System.Collections.Generic;
using Veho.Sequence;
using Veho.Vector;

namespace Aryth {
  public static class FlopperFactory {
    public static IEnumerable<T> FiniteFlopper<T>(this T[] vec) {
      var rand = new Random();
      var hi = vec.Length;
      while (--hi >= 0) yield return vec.Swap(rand.Next(hi), hi);
    }
    public static IEnumerator<T> InfiniteFlopper<T>(this T[] vec, T df = default) {
      var rand = new Random();
      var hi = vec.Length;
      while (--hi >= 0) yield return vec.Swap(rand.Next(hi), hi);
      while (true) yield return df;
    }
    public static IEnumerable<T> FiniteFlopper<T>(this IList<T> vec) {
      var rand = new Random();
      var hi = vec.Count;
      while (--hi >= 0) yield return vec.Swap(rand.Next(hi), hi);
    }
    public static IEnumerator<T> InfiniteFlopper<T>(this IList<T> vec, T df = default) {
      var rand = new Random();
      var hi = vec.Count;
      while (--hi >= 0) yield return vec.Swap(rand.Next(hi), hi);
      while (true) yield return df;
    }
  }
}