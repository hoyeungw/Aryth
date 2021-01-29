namespace Aryth.Bounds.Helpers {
  public static class Assorter {
    public static (double x, double y) Assort<T>(T x) {
      var s = x.ToString();
      return double.TryParse(s, out var n)
        ? (double.NaN, n)
        : (s.StringValue(), double.NaN);
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

    public static void ExpandBound(ref (double min, double max)? bound, double value) {
      if (double.IsNaN(value)) return;
      if (bound == null) bound = (value, value);
      else {
        var pair = bound.Value;
        if (value > pair.max) pair.max = value;
        else if (value < pair.min) pair.min = value;
        bound = pair;
      }
    }
  }
}