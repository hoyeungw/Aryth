using static System.Math;

namespace Aryth.Polar {
  public static class Util {
    public static double DegreeToRadian(double degree) => degree * PI / 180;
    public static double RadianToDegree(double radian) => radian * 180 / PI;
    public static (double r, double θ) DegreeToRadian(this (double r, double θ) polar) => (polar.r, DegreeToRadian(polar.θ));
    public static (double r, double θ) RadianToDegree(this (double r, double θ) polar) => (polar.r, RadianToDegree(polar.θ));
    public static (double r, double θ) Rotate(this (double r, double θ) polar, double degree) {
      var θ = polar.θ + degree;
      while (θ > 360) θ -= 360;
      while (θ < 0) θ += 360;
      return (polar.r, θ);
    }
  }
}