using NUnit.Framework;
using Spare;
using Veho.Mutable.Matrix;
using MU = Veho.Mutable;
using System.Collections.Generic;
using Aryth.Coord;
using Aryth.Polar;
using Aryth.Polar.Beta;
using Veho.Sequence;

namespace Aryth.Test.Polar {
  [TestFixture]
  public class FoliumTests {
    [Test]
    public void TrifoliumTest() {
      var matrix = MU::Mat.Init((11, 11), (x, y) => ((y - 5d) * 20, (5d - x) * 20));
      matrix.ToMatrix().Deco().Says("matrix");
      var foliums = matrix
                    .Fold()
                    .Map(x => x.CartesianToPolar())
                    .RhodoneaFolios((100, 45), 3, 0.0015)
                    .Map(x => x.PolarToCartesian().RoundD1());
      // trifoliums.Deco().Says("trifolium", trifoliums.Count.ToString());
      var result = matrix.Map(x => foliums.Contains(x) ? "+" : " ");
      result.ToMatrix().Deco().Says("result");
    }
    [Test]
    public void QuadrifoliumTest() {
      var matrix = MU::Mat.Init((11, 11), (x, y) => ((y - 5d) * 20, (5d - x) * 20));
      matrix.ToMatrix().Deco().Says("matrix");
      var foliums = matrix
                    .Fold()
                    .Map(x => x.CartesianToPolar())
                    .RhodoneaFolios((100, 45), 4)
                    .Map(x => x.PolarToCartesian().RoundD1());
      var result = matrix.Map(x => foliums.Contains(x) ? "+" : " ");
      result.ToMatrix().Deco().Says("result");
    }
  }

  public static class Util {
    public static List<T> Fold<T>(this List<List<T>> nested) =>
      nested.Fold((accum, curr) => {
        accum.AddRange(curr);
        return accum;
      }, new List<T>());
  }
}