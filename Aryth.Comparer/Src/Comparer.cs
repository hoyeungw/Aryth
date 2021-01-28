namespace Aryth.Comparer {
  public static class Comparer {
    public static int NumAsc(this double a, double b) => a < b ? -1 : a > b ? 1 : 0;
    public static int NumDsc(this double a, double b) => a > b ? -1 : a < b ? 1 : 0;
  }
}