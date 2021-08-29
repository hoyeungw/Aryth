using System.Collections.Generic;
using static System.Math;

namespace Aryth.Coord {
  public static class Calculators {
    public static List<(double x, double y)> AdjacentOf(this List<(double x, double y)> list, (double x, double y) point, double radius) {
      var (x, y) = point;
      return list.FindAll(pn => Sqrt(Pow(pn.x - x, 2) + Pow(pn.y - y, 2)) < radius);
    }
  }
}