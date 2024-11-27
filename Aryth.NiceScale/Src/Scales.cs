using System;
using static System.Math;

namespace Aryth {
  public static class Scales {
    public static (double min, double max, double step) NiceScale(this (double min, double max) bound, int ticks = 10) {
      var (lo, hi) = bound;
      var delta = NiceNum(hi - lo, false);
      var step = NiceNum(delta / (ticks - 1), true);
      double min = Floor(lo / step) * step, max = Ceiling(hi / step) * step;
      return (min, max, step);
    }

    public static double[] NiceTicks(this (double min, double max) bound, int ticks = 10) {
      var (min, max, step) = bound.NiceScale(ticks);
      return Ticks(min, step, (int)Round((max - min) / step));
    }

    [Obsolete("Use NickTicks")]
    public static double[] NiceLabels(this (double min, double max) bound, int ticks = 10) => bound.NiceTicks(ticks);

    public static double[] Ticks(double lo, double step, int gaps) {
      var vec = new double[gaps + 1];
      var i = 0;
      do {
        vec[i++] = lo;
        lo += step;
      } while (i <= gaps);
      return vec;
    }

    public static double NiceNum(double range, bool round) {
      var expon = Floor(Log10(range));
      var frac = range / Pow(10, expon);
      var niceFrac = round ? NiceFractionRounded(frac) : NiceFraction(frac);
      return niceFrac * Pow(10, expon);
    }

    public static double NiceFractionRounded(double frac) =>
      frac < 1.5 ? 1 :
      frac < 3   ? 3 :
      frac < 7   ? 5 :
                   10;

    public static double NiceFraction(double frac) =>
      frac <= 1 ? 1 :
      frac <= 2 ? 2 :
      frac <= 5 ? 5 :
                  10;
  }
}