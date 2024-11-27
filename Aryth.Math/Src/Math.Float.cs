using static System.Math;

namespace Aryth {
  public static partial class Math {
    public static int TrailExp(this float num, int trail) {
      string sci = $"0.{new string('0', trail)}E0";
      return num.TrailExp(sci);
    }
    public static int TrailExp(this float num, string sci = "0.000000E0") {
      var groups = TrailRegex.Match(num.ToString(sci)).Groups;
      string n = groups[1].Value, d = groups[2].Value;
      var i = n.Length;
      while (--i >= 0) {
        if (n[i] != '0') return int.Parse(d) - ++i;
      }
      return 0;
    }

    public static int IntExp(float x) => x == 0 ? 0 : (int)Log10(x);
    public static float RoundD1(float x) => (float)Round(x * 10) / 10;
    public static float RoundD2(float x) => (float)Round(x * E2) / E2;
    public static float RoundD3(float x) => (float)Round(x * E3) / E3;
    public static float RoundD4(float x) => (float)Round(x * E4) / E4;

    // public static bool Has(this (float min, float max) bin, float num, Open open = Open.None) {
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
    public static bool Hold(this (float min, float max) bin, float num) => bin.min <= num && num <= bin.max;

    /// <summary>for open</summary>
    public static bool Allow(this (float min, float max) bin, float num) => bin.min < num && num < bin.max;

    public static float Limit(this (float min, float max) bin, float num) {
      var (min, max) = bin;
      if (num < min) return min;
      if (num > max) return max;
      return num;
    }
    public static float Restrict(this (float min, float max) period, float num) {
      var (min, max) = period;
      var delta = max - min;
      while (num < min) num += delta;
      while (num > max) num -= delta;
      return num;
    }
    public static float Limit(float num, float max) {
      if (num < 0) return 0;
      if (num > max) return max;
      return num;
    }
    public static float Restrict(float num, float max) {
      while (num < 0) num += max;
      while (num > max) num -= max;
      return num;
    }
  }
}