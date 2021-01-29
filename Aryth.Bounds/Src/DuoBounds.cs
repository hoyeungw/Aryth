using Aryth.Bounds.Helpers;
using Veho.Entries;
using Veho.Vector;
using Veho.Matrix;
using Veho.Matrix.Columns;
using Veho.Matrix.Rows;

// using Bound = System.Nullable<(double, double)>;
// using VectorAndBound = System.ValueTuple<double[], (double, double)?>;

namespace Aryth.Bounds {
  public static class DuoBounds {
    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBound<T>(this T[] vector) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = vector
                       .Map(x => Assorter.AssortExpandEntryBound(ref bdX, ref bdY, x))
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }

    public static ((double[,] mat, (double min, double max)?) x, (double[,] mat, (double min, double max)?) y) DuoBound<T>(this T[,] matrix) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Map(x => Assorter.AssortExpandEntryBound(ref bdX, ref bdY, x))
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }

    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBoundRow<T>(this T[,] matrix, int x, int w = 0) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Row(x, v => Assorter.AssortExpandEntryBound(ref bdX, ref bdY, v), w)
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }

    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBoundColumn<T>(this T[,] matrix, int y, int h = 0) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Column(y, v => Assorter.AssortExpandEntryBound(ref bdX, ref bdY, v), h)
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }
  }
}