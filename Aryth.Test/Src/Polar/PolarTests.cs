using System;
using NUnit.Framework;
using System.Collections.Generic;
using Aryth.Coord;
using Aryth.Polar.Beta;
using Veho;

namespace Aryth.Test.Polar {
  [TestFixture]
  public class PolarTests {
    [Test]
    public static void TestBeta() {
      List<(double x, double y)> candidates = Seq.From(
        (100.0, 0.0),
        (86.6, 50.0),
        (50.0, 86.6),
        (0.0, 100.0)
      );
      foreach (var coord in candidates) {
        var polar = coord.CartesianToPolar().RoundD1();
        var coord2 = polar.PolarToCartesian().RoundD1();
        Console.WriteLine($">> [coord] {coord} [polar] {polar} [coord2] {coord2}");
      }
    }
  }
}