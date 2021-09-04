using static System.Math;

namespace Aryth.Polar {
  public static class Foliates {
    public static double FoliateRadius(this (double r, double θ) verge, double currAngle, int petals = 3) {
      return verge.r * Cos(petals * (currAngle - verge.θ) * PI / 180);
    }
    public static bool InFoliate(this (double r, double θ) polar, (double r, double θ) verge, int petals = 3) {
      return polar.r <= verge.FoliateRadius(polar.θ, petals);
    }
  }
}