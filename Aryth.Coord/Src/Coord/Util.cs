using static System.Math;

namespace Aryth.Coord {
  public static class Util {
    public static double Radius(this (double x, double y) coord) {
      var (x, y) = coord;
      return Sqrt(x * x + y * y);
    }
    public static (double x, double y) RoundD1(this (double x, double y) coord) => (Math.RoundD1(coord.x), Math.RoundD1(coord.y));
    public static (double x, double y) RoundD2(this (double x, double y) coord) => (Math.RoundD2(coord.x), Math.RoundD2(coord.y));
    public static (double x, double y) RoundD3(this (double x, double y) coord) => (Math.RoundD3(coord.x), Math.RoundD3(coord.y));
    public static (double x, double y) RoundD4(this (double x, double y) coord) => (Math.RoundD4(coord.x), Math.RoundD4(coord.y));
  }
}