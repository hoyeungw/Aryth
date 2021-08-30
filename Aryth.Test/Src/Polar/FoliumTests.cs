﻿using System.Collections.Generic;
using Aryth.Coord;
using Aryth.Polar;
using Aryth.Polar.Beta;
using NUnit.Framework;
using Spare;
using Veho.Mutable.Matrix;
using Veho.Sequence;
using MU = Veho.Mutable;

namespace Aryth.Test.Polar {
  [TestFixture]
  public class FoliumTests {
    public static List<List<(double x, double y)>> Matrix = MU.Mat.Init((11, 11), (x, y) => ((y - 5d) * 20, (5d - x) * 20));

    [Test]
    public void DensityFoliumTest() {
      var candidates = Matrix
                       .Flat()
                       .Map(x => x.CartesianToPolar());
      var bifoliums = candidates
                      .RhodoneaFolios((100, 45), 2, 0.0015)
                      .Map(x => x.PolarToCartesian().RoundD1());
      var trifoliums = candidates
                       .RhodoneaFolios((100, 45), 3, 0.0015)
                       .Map(x => x.PolarToCartesian().RoundD1());
      var quadrifoliums = candidates
                          .RhodoneaFolios((100, 45), 4, 0.0015)
                          .Map(x => x.PolarToCartesian().RoundD1());
      var pentafoliums = candidates
                         .RhodoneaFolios((100, 45), 5, 0.0015)
                         .Map(x => x.PolarToCartesian().RoundD1());
      var hexafoliums = candidates
                        .RhodoneaFolios((100, 45), 6, 0.0015)
                        .Map(x => x.PolarToCartesian().RoundD1());
      Matrix.Map(x => bifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("bifoliums");
      Matrix.Map(x => trifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("trifoliums");
      Matrix.Map(x => quadrifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("quadrifoliums");
      Matrix.Map(x => pentafoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("pentafoliums");
      Matrix.Map(x => hexafoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("hexafoliums");
    }
    [Test]
    public void FoliumTest() {
      Matrix.ToMatrix().Deco().Says("matrix");
      var candidates = Matrix
                       .Flat()
                       .Map(x => x.CartesianToPolar());
      var bifoliums = candidates
                      .RhodoneaFolios((100, 45), 2) // 0.0015
                      .Map(x => x.PolarToCartesian().RoundD1());
      var trifoliums = candidates
                       .RhodoneaFolios((100, 45), 3) // 0.0015
                       .Map(x => x.PolarToCartesian().RoundD1());
      var quadrifoliums = candidates
                          .RhodoneaFolios((100, 45), 4) // 0.0015
                          .Map(x => x.PolarToCartesian().RoundD1());
      var pentafoliums = candidates
                         .RhodoneaFolios((100, 45), 5) // 0.0015
                         .Map(x => x.PolarToCartesian().RoundD1());
      var hexafoliums = candidates
                        .RhodoneaFolios((100, 45), 6) // 0.0015
                        .Map(x => x.PolarToCartesian().RoundD1());
      Matrix.Map(x => bifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("bifoliums");
      Matrix.Map(x => trifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("trifoliums");
      Matrix.Map(x => quadrifoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("quadrifoliums");
      Matrix.Map(x => pentafoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("pentafoliums");
      Matrix.Map(x => hexafoliums.Contains(x) ? "+" : " ").ToMatrix().Deco().Says("hexafoliums");
    }
  }
}