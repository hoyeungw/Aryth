using System;
using static System.Math;

namespace Aryth {
  public static partial class Math {
    public static int TrailExp(double num) => TrailExpOf(num, Math.SciNote);

    public static int TrailExpBy(double num, int trail) => TrailExpOf(num, $"0.{new string('0', trail)}E0");

    private static int TrailExpOf(double num, string sci) {
      var literal = num.ToString(sci);
      var groups = TrailRegex.Match(literal).Groups;
      string n = groups[1].Value, d = groups[2].Value;
      var i = n.Length;
      while (--i >= 0)
        if (n[i] != '0')
          return int.Parse(d) - ++i;
      return 0;
    }

    public static int IntExp(double x) => x == 0 ? 0 : (int)Log10(x);

    public static double Sq(double x) => Pow(x, 2);

    public static double RoundD1(double x) => Round(x * 10) / 10;
    public static double RoundD2(double x) => Round(x * E2) / E2;
    public static double RoundD3(double x) => Round(x * E3) / E3;
    public static double RoundD4(double x) => Round(x * E4) / E4;

    public static bool AlmostEqual(double x, double y, double epsilon) => Abs(x - y) < epsilon;
    // public static bool Has(this (double min, double max) bin, double num, Open open = Open.None) {
    //   var (minOpen, maxOpen) = open.Decode(); // (((byte)open >> 0 & 0b1) == 0b1, ((byte)open >> 1 & 0b1) == 0b1);
    //   return minOpen
    //     ? maxOpen
    //       ? bin.min < num && num < bin.max
    //       : bin.min < num && num <= bin.max
    //     : maxOpen
    //       ? bin.min <= num && num < bin.max
    //       : bin.min <= num && num <= bin.max;
    // }

    /// <summary>for closed</summary>
    public static bool Hold(this (double min, double max) bin, double num) => bin.min <= num && num <= bin.max;

    /// <summary>for open</summary>
    public static bool Allow(this (double min, double max) bin, double num) => bin.min < num && num < bin.max;

    public static double Limit(this (double min, double max) bin, double num) {
      var (min, max) = bin;
      if (num < min) return min;
      if (num > max) return max;
      return num;
    }

    public static double Restrict(this (double min, double max) period, double num) {
      var (min, max) = period;
      var delta = max - min;
      while (num < min) num += delta;
      while (num > max) num -= delta;
      return num;
    }

    public static double Limit(double num, double max) {
      if (num < 0) return 0;
      if (num > max) return max;
      return num;
    }
    public static double Restrict(double num, double max) {
      while (num < 0) num += max;
      while (num > max) num -= max;
      return num;
    }

    [Obsolete("Use Has / Hold / Allow instead")]
    public static bool HasOpen(this (double min, double max) bin, double num) => bin.min < num && num < bin.max;

    [Obsolete("Use Has / Hold / Allow instead")]
    public static bool HasLOpen(this (double min, double max) bin, double num) => bin.min < num && num <= bin.max;

    [Obsolete("Use Has / Hold / Allow instead")]
    public static bool HasROpen(this (double min, double max) bin, double num) => bin.min <= num && num < bin.max;
  }
}