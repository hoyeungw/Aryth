using System;
using Aryth.Coord;
using Aryth.Polar.Beta;
using NUnit.Framework;

namespace Aryth.Test.Polar {
  [TestFixture]
  public partial class PolarTests {
    [Test]
    public static void CartesianAndPolarConvertTest() {
      foreach (var coord in Candidates) {
        var polar = coord.CartesianToPolar().RoundD1();
        var coord2 = polar.PolarToCartesian().RoundD1();
        Console.WriteLine($">> [coord] {coord} [polar] {polar} [coord2] {coord2}");
      }
    }
  }
}