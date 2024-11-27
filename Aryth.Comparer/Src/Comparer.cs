using System;

namespace Aryth {
  public static class Comparer {
    public static int NumAsc(this double a, double b) => a < b ? -1 : a > b ? 1 : 0; // a.CompareTo(b)

    public static int NumDes(this double a, double b) => a > b ? -1 : a < b ? 1 : 0; // b.CompareTo(a)

    [Obsolete("Use NumDes instead")]
    public static int NumDsc(this double a, double b) => a > b ? -1 : a < b ? 1 : 0; // b.CompareTo(a)

    public static int Asc<T>(this T a, T b) where T : IComparable<T> => a.CompareTo(b);
    public static int Des<T>(this T a, T b) where T : IComparable<T> => b.CompareTo(a);

    // public static int NumAsc(this int a, int b) => a.CompareTo(b);
    // public static int NumDes(this int a, int b) => b.CompareTo(a);
    // public static int NumAsc(this float a, float b) => a.CompareTo(b);
    // public static int NumDes(this float a, float b) => b.CompareTo(a);
    // public static int NumAsc(this byte a, byte b) => a.CompareTo(b);
    // public static int NumDes(this byte a, byte b) => b.CompareTo(a);
    // public static int NumAsc(this long a, long b) => a.CompareTo(b);
    // public static int NumDes(this long a, long b) => b.CompareTo(a);
    // public static int NumAsc(this decimal a, decimal b) => a.CompareTo(b);
    // public static int NumDes(this decimal a, decimal b) => b.CompareTo(a);
  }
}