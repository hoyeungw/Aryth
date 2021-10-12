using System;
using NUnit.Framework;
using Spare;
using Veho;
using static System.Math;

namespace Aryth.Test.NiceScale {
  public static class Methods {
    public static (double min, double max) GetExponents(this (double min, double max) bound) {
      var (min, max) = bound;
      var minExpon = min == 0 ? 0 : Floor(Log10(Abs(min)));
      var maxExpon = max == 0 ? 0 : Floor(Log10(Abs(max)));
      // Console.WriteLine($">> [NiceNum] (min, max) = ({min}, {max}) [minExpon] ({minExpon}) [maxExpon] ({maxExpon})");
      return (minExpon, maxExpon);
    }
  }

  [TestFixture]
  public class NiceScaleTests {
    [Test]
    public void Test() {
      var candidates = Vec.From(
        (-0.05, +0.15),
        (0, 1),
        (0, 10),
        (3.14, 9.99),
        (-3.14, 12),
        (13.14, 79.9),
        (14024, 17756),
        (13299, 13304)
      );

      foreach (var valueTuple in candidates) {
        valueTuple.NiceScale().Says(valueTuple.ToString());
      }
    }
  }
}