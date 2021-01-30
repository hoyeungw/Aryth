using Aryth.Bounds.Helpers;
using Veho.Matrix;
using Veho.Matrix.Columns;
using Veho.Matrix.Rows;
using Veho.Vector;

namespace Aryth.Bounds {
  public static class SoleBounds {
    public static (double[] vec, (double min, double max)? bound) DuoBound<T>(this T[] vector) {
      (double min, double max)? bound = null;
      var vec = vector.Map(x => Assorter.AssortExpandBound(ref bound, x));
      return (vec, bound);
    }

    public static (double[,] mat, (double min, double max)? bound) DuoBound<T>(this T[,] matrix) {
      (double min, double max)? bound = null;
      var vec = matrix.Map(x => Assorter.AssortExpandBound(ref bound, x));
      return (vec, bound);
    }

    public static (double[] vec, (double min, double max)? bound) DuoBoundRow<T>(this T[,] matrix, int x, int w = 0) {
      (double min, double max)? bound = null;
      var vec = matrix.Row(x, v => Assorter.AssortExpandBound(ref bound, v), w);
      return (vec, bound);
    }

    public static (double[] vec, (double min, double max)? bound) DuoBoundColumn<T>(this T[,] matrix, int y, int h = 0) {
      (double min, double max)? bound = null;
      var vec = matrix.Column(y, v => Assorter.AssortExpandBound(ref bound, v), h);
      return (vec, bound);
    }
  }
}