using Aryth.Coord;
using static System.Math;
using static Aryth.Polar.Util;

namespace Aryth.Polar.Alpha {
  public static class Converters {
    public static (double x, double y) PolarToCartesian(this (double r, double θ) polar) {
      var (r, θ) = polar;
      var radiant = DegreeToRadian(θ);
      var x = Cos(radiant) * r;
      var y = Sin(radiant) * r;
      return (x, y);
    }
    public static (double r, double θ) CartesianToPolar(this (double x, double y) coord) {
      return (coord.Radius(), coord.PolarDegree());
    }

    public static double PolarDegree(this (double x, double y) coord) {
      return RadianToDegree(Atan2(coord.y, coord.x));
    }
  }
}