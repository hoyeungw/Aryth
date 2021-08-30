using System.Collections.Generic;
using static System.Math;

namespace Aryth.Polar {
  public static class Calculator {
    public static readonly double TrifoliumArea = 0.25;
    public static readonly double QuadrifoliumArea = 0.5;
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
    public static List<(double r, double θ)> RhodoneaFolios(this List<(double r, double θ)> list, (double r, double θ) rimMark, int pedals = 3) {
      var target = list.FindAll(polar => polar.r <= rimMark.FoliateRadius(polar.θ, pedals));
      return target;
    }
    public static List<(double r, double θ)> RhodoneaFolios(this List<(double r, double θ)> list, (double r, double θ) rimMark, int pedals, double density) {
      var area = TrifoliumArea * PI * Pow(rimMark.r, 2);
      var desired = (int)Round(density * area);
      var target = new List<(double r, double θ)>(desired);
      foreach (var polar in list.FiniteFlopper()) {
        if (target.Count >= desired) break;
        if (polar.r <= rimMark.FoliateRadius(polar.θ, pedals)) target.Add(polar);
      }
      return target;
    }
  }
}