using System;
using System.Collections.Generic;
using Aryth.Coord;
using Aryth.Polar.Beta;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Polar {
  [TestFixture]
  public partial class PolarTests {
    [Test]
    public static void CartesianAndPolarConvertTest() {
      foreach (var coord in PolarTests.Candidates) {
        var polar = coord.CartesianToPolar().RoundD1();
        var coord2 = polar.PolarToCartesian().RoundD1();
        Console.WriteLine($">> [coord] {coord} [polar] {polar} [coord2] {coord2}");
      }
    }
  }
}