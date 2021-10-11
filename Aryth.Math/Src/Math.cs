using static System.Math;

namespace Aryth {
  public static class Math {
    public static int IntExp(float x) => x == 0 ? 0 : (int)Log10(x);
    public static int IntExp(double x) => x == 0 ? 0 : (int)Log10(x);

    public static float E2 = 100;
    public static float E3 = 1000;
    public static float E4 = 10000;

    public static float RoundD1(float x) => (float)Round(x * 10) / 10;
    public static float RoundD2(float x) => (float)Round(x * E2) / E2;
    public static float RoundD3(float x) => (float)Round(x * E3) / E3;
    public static float RoundD4(float x) => (float)Round(x * E4) / E4;

    public static double RoundD1(double x) => Round(x * 10) / 10;
    public static double RoundD2(double x) => Round(x * E2) / E2;
    public static double RoundD3(double x) => Round(x * E3) / E3;
    public static double RoundD4(double x) => Round(x * E4) / E4;

    public static bool AlmostEqual(double x, double y, double epsilon) => Abs(x - y) < epsilon;

    public static bool HasOpen(this (double min, double max) interval, double num) => interval.min < num && num < interval.max;
    public static bool HasLOpen(this (double min, double max) interval, double num) => interval.min < num && num <= interval.max;
    public static bool HasROpen(this (double min, double max) interval, double num) => interval.min <= num && num < interval.max;
    public static bool Has(this (double min, double max) interval, double num) => interval.min <= num && num <= interval.max;
    public static double Restrict(this (double min, double max) interval, double num) {
      var (min, max) = interval;
      return num < min ? min : max < num ? max : num;
    }
  }
}