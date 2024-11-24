using static System.Math;

namespace Aryth {
  public static partial class Math {
    public static int TrailExp(this double num, int trail) {
      string sci = $"0.{new string('0', trail)}E0";
      return num.TrailExp(sci);
    }
    public static int TrailExp(this double num, string sci = "0.000000E0") {
      var groups = TrailRegex.Match(num.ToString(sci)).Groups;
      string n = groups[1].Value, d = groups[2].Value;
      var i = n.Length;
      while (--i >= 0) {
        if (n[i] != '0') return int.Parse(d) - ++i;
      }
      return 0;
    }

    public static int IntExp(double x) => x == 0 ? 0 : (int)Log10(x);

    public static double RoundD1(double x) => Round(x * 10) / 10;
    public static double RoundD2(double x) => Round(x * E2) / E2;
    public static double RoundD3(double x) => Round(x * E3) / E3;
    public static double RoundD4(double x) => Round(x * E4) / E4;

    public static bool AlmostEqual(double x, double y, double epsilon) => Abs(x - y) < epsilon;

    public static bool HasOpen(this (double min, double max) interval, double num) => interval.min < num && num < interval.max;
    public static bool HasLOpen(this (double min, double max) interval, double num) => interval.min < num && num <= interval.max;
    public static bool HasROpen(this (double min, double max) interval, double num) => interval.min <= num && num < interval.max;
    public static bool Has(this (double min, double max) interval, double num) => interval.min <= num && num <= interval.max;
    public static double Limit(this (double min, double max) interval, double value) {
      var (min, max) = interval;
      if (value < min) return min;
      if (value > max) return max;
      return value;
    }

    public static double Restrict(this (double min, double max) periodic, double value) {
      var (min, max) = periodic;
      var delta = max - min;
      while (value < min) value += delta;
      while (value > max) value -= delta;
      return value;
    }

    public static double Limit(double value, double max) {
      if (value < 0) return 0;
      if (value > max) return max;
      return value;
    }
    public static double Restrict(double value, double max) {
      while (value < 0) value += max;
      while (value > max) value -= max;
      return value;
    }
  }
}