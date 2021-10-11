using System;
using Aryth.Bounds;
using NUnit.Framework;
using Spare;
using Texting;
using Veho;
using static System.Math;
using static Aryth.Math;

namespace Aryth.Test {
  public static class Methods {
    public static string FindNumberFormat(this double[] vec) {
      var soleBound = vec.Bound();
      if (soleBound.HasValue) {
        var (min, max) = soleBound.Value;
        var outer = Max(Abs(max), Abs(min));
        var inner = Min(Abs(max), Abs(min));
        var outerExp = IntExp(outer);
        var innerExp = IntExp(inner);
        Console.WriteLine($">> [inner] {inner} ({innerExp}) [outer] {outer} ({outerExp})");
        if (outer == 0) return null;
        if (outerExp > 0) { return @"_ * #,##0_ ;_ * -#,##0_ ;_ * ""-""_ ;_ @_ "; }
        if (outerExp < 0) { return $"#,##0.{'0'.Repeat(Abs(outerExp))}_ "; }
      }
      return null;
    }
  }

  [TestFixture]
  public class NumberFormatTests {
    [Test]
    public void FindNumberFormatTest() {
      var candidates = Vec.From(
        Vec.From<double>(0, 1, 2, 3, 4, 5),
        Vec.From<double>(0.1, 0.07, 0.01, 0.05, 0),
        Vec.From<double>(-0.1, -0.07, 0.01, 0.05, 0),
        Vec.From<double>(1000, 10000, 100000, 0, 0),
        Vec.From<double>(-70, -80, -90, -100, -120)
      );
      foreach (var candidate in candidates) {
        Console.WriteLine($">> [{candidate.Deco()}] {candidate.FindNumberFormat()}");
      }
    }
  }
}