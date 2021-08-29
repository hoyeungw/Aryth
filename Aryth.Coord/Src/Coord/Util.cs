using static System.Math;

namespace Aryth.Coord {
  public static class CoordUtil {
    public static double Radius(this (double x, double y) coord) {
      var (x, y) = coord;
      return Sqrt(x * x + y * y);
    }
    public static (double x, double y) RoundD1(this (double x, double y) coord) =>
      (Math.RoundD1(coord.x), Math.RoundD1(coord.y));
  }
}