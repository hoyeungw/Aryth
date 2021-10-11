using System.Collections.Generic;
using Aryth.Bounds.Utils;
using Veho.Vector;

namespace Aryth.Bounds {
  public static partial class SoleBounds {
    public static (double[] vec, (double min, double max)? bound) SoleBound<T>(this IReadOnlyList<T> vector) {
      (double min, double max)? bound = null;
      var vec = vector.Map(x => Assorter.AssortExpandBound(ref bound, x));
      return (vec, bound);
    }
  }
}