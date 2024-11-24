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

    public static float Limit(this (float min, float max) interval, float value) {
      var (min, max) = interval;
      if (value < min) return min;
      if (value > max) return max;
      return value;
    }
    public static float Restrict(this (float min, float max) periodic, float value) {
      var (min, max) = periodic;
      var delta = max - min;
      while (value < min) value += delta;
      while (value > max) value -= delta;
      return value;
    }
    public static float Limit(float value, float max) {
      if (value < 0) return 0;
      if (value > max) return max;
      return value;
    }
    public static float Restrict(float value, float max) {
      while (value < 0) value += max;
      while (value > max) value -= max;
      return value;
    }
  }
}