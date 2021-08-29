using static System.Math;

namespace Aryth.Polar {
  public static class Util {
    public static double DegreeToRadian(double degree) => degree * PI / 180;
    public static double RadianToDegree(double radian) => radian * 180 / PI;
  }
}