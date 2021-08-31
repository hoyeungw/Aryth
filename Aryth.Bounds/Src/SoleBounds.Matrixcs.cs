using Aryth.Bounds.Utils;
using Veho.Columns;
using Veho.Matrix;
using Veho.Rows;

namespace Aryth.Bounds {
  public static partial class SoleBounds {
    public static (double[,] mat, (double min, double max)? bound) SoleBound<T>(this T[,] matrix) {
      (double min, double max)? bound = null;
      var vec = matrix.Map(x => Assorters.AssortExpandBound(ref bound, x));
      return (vec, bound);
    }

    public static (double[] vec, (double min, double max)? bound) SoleBoundRow<T>(this T[,] matrix, int x, int w = 0) {
      (double min, double max)? bound = null;
      var vec = matrix.Row(x, v => Assorters.AssortExpandBound(ref bound, v), w);
      return (vec, bound);
    }

    public static (double[] vec, (double min, double max)? bound) SoleBoundColumn<T>(this T[,] matrix, int y, int h = 0) {
      (double min, double max)? bound = null;
      var vec = matrix.Column(y, v => Assorters.AssortExpandBound(ref bound, v), h);
      return (vec, bound);
    }
  }
}