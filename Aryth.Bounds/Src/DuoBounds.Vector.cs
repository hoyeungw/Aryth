using System.Collections.Generic;
using Aryth.Bounds.Utils;
using Veho;
using Veho.Vector;

namespace Aryth.Bounds {
  public static partial class DuoBounds {
    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBound<T>(this IReadOnlyList<T> vector) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = vector
                       .Map(x => Assorters.AssortExpandEntryBound(ref bdX, ref bdY, x))
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }
  }
}