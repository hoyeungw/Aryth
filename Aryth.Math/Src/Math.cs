using SM = System.Math;

namespace Aryth.Math {
  public static class Math {
    public static int IntExp(float x) => (int) SM.Log10(x);
    public static int IntExp(double x) => (int) SM.Log10(x);
  }
}