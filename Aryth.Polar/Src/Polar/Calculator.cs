using System.Collections.Generic;

namespace Aryth.Polar {
  public static class Calculator {

    public static (double r, double θ) Complementary(this (double r, double θ) polar) => polar.Rotate(180);
    public static ((double r, double θ) lower, (double r, double θ) upper) SplitComplementary(this (double r, double θ) polar, double deviation) {
      var complementary = polar.Rotate(180);
      var lower = complementary.Rotate(-deviation);
      var upper = complementary.Rotate(+deviation);
      return (lower, upper);
    }
    public static ((double r, double θ) lower, (double r, double θ) upper) Triadic(this (double r, double θ) polar, double deviation) {
      var lower = polar.Rotate(+120);
      var upper = polar.Rotate(-120);
      return (lower, upper);
    }
    public static List<(double r, double θ)> Analogous(this (double r, double θ) polar, double delta, int count) {
      var list = new List<(double, double)>(count);
      for (var i = 0; i < count; i++) list.Add(polar = polar.Rotate(delta));
      return list;
    }
    

  }
}