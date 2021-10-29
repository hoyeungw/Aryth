using static System.Math;

namespace Aryth {
  public static partial class Pol {
    public static double DegreeToRadian(double degree) => degree * Pi / 180;
    public static double RadianToDegree(double radian) => radian * 180 / Pi;
    public static (double r, double θ) DegreeToRadian(this (double r, double θ) polar) => (polar.r, DegreeToRadian(polar.θ));
    public static (double r, double θ) RadianToDegree(this (double r, double θ) polar) => (polar.r, RadianToDegree(polar.θ));

    public static double Distance(double θa, double θb) {
      var abs = Abs(θa - θb);
      return Min(abs, Abs(360 - abs));
    }
    public static float Minus(float θa, float θb) {
      float rawDf = θa - θb, posDf = Abs(rawDf), negDf = Abs(360f - posDf);
      if (posDf <= negDf) {
        return rawDf > 0 ? posDf : -posDf;
      }
      else {
        return rawDf < 0 ? negDf : -negDf;
      }
    }
    public static bool Near(double θa, double θb, double epsilon) {
      return Distance(θa, θb) < epsilon;
    }
    public static bool Contains(this (double a, double b) interval, double θ) {
      var (a, b) = interval;
      return a <= b ? a < θ && θ < b : a < θ || θ < b;
    }
    public static (double r, double θ) Rotate(this (double r, double θ) polar, double degree) {
      var θ = polar.θ + degree;
      return (polar.r, Restrict(θ));
    }
    public static double Restrict(double θ) {
      while (θ > 360) θ -= 360;
      while (θ < 0) θ += 360;
      return θ;
    }
  }
}