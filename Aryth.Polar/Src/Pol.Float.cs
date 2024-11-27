using static System.Math;

namespace Aryth {
  public static partial class Pol {
    public const float Pi = (float)System.Math.PI;
    public static float DegreeToRadian(float degree) => degree * Pol.Pi / 180;
    public static float RadianToDegree(float radian) => radian * 180 / Pol.Pi;

    public static (float r, float θ) DegreeToRadian(this (float r, float θ) polar) => (polar.r, DegreeToRadian(polar.θ));
    public static (float r, float θ) RadianToDegree(this (float r, float θ) polar) => (polar.r, RadianToDegree(polar.θ));

    public static float Distance(float θa, float θb) {
      var abs = Abs(θa - θb);
      return Min(abs, Abs(360 - abs));
    }

    public static float Add(float θa, float θb) {
      return Restrict(θa + θb);
    }

    public static bool Near(float θa, float θb, float epsilon) {
      return Distance(θa, θb) < epsilon;
    }
    public static bool Contains(this (float a, float b) bin, float θ) {
      var (a, b) = bin;
      return a <= b
        ? a < θ && θ < b
        : a < θ || θ < b;
    }
    public static (float r, float θ) Rotate(this (float r, float θ) polar, float degree) {
      var θ = polar.θ + degree;
      return (polar.r, Restrict(θ));
    }
    public static float Restrict(float θ) {
      while (θ > 360) θ -= 360;
      while (θ < 0) θ += 360;
      return θ;
    }
  }
}