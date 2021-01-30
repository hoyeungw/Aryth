using Texting.Value;
using Typen.Numeral;

namespace Aryth.Bounds.Helpers {
  public static partial class Assorter {
    public static (double x, double y) Assort<T>(T x) {
      var s = x.ToString();
      return double.TryParse(s, out var n)
        ? (double.NaN, n)
        : (s.StringValue(), double.NaN);
    }

    public static double AssortExpandBound<T>(ref (double min, double max)? boundX, T value) {
      var assorted = value.ToF64();
      ExpandBound(ref boundX, assorted);
      return assorted;
    }

    public static (double x, double y) AssortExpandEntryBound<T>(
      ref (double min, double max)? boundX,
      ref (double min, double max)? boundY,
      T value
    ) {
      var assorted = Assort(value);
      ExpandBound(ref boundX, assorted.x);
      ExpandBound(ref boundY, assorted.y);
      return assorted;
    }

    public static void ExpandBound(ref (double min, double max)? bound, double n) {
      if (double.IsNaN(n)) return;
      if (bound == null) bound = (n, n);
      else {
        var pair = bound.Value;
        if (n > pair.max) { pair.max = n; } else if (n < pair.min) { pair.min = n; }
        bound = pair;
      }
    }
  }
}