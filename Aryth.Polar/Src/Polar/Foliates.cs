using static System.Math;

namespace Aryth.Polar {
  public static class Foliates {
    public static double FoliateRadius(this (double r, double θ) rimMark, double currAngle, int petals = 3) {
      return rimMark.r * Cos(petals * (currAngle - rimMark.θ) * PI / 180);
    }
  }
}