using System;
using Veho.Vector;
using Veho.Matrix;
using Veho.Matrix.Columns;
using Veho.Matrix.Rows;

namespace Aryth.Bounds {
  public static class Bounders {
    public static (T min, T max) InitBound<T>(T value) => (value, value);
    public static (T min, T max)? Bound<T>(this T[] vec) where T : IComparable<T> => vec.Length == 0
      ? ((T min, T max)?) null
      : vec.Fold(
        (b, x) => {
          if (x.CompareTo(b.max) > 0) { b.max = x; }
          else if (x.CompareTo(b.min) < 0) { b.min = x; }
          return b;
        },
        InitBound
      );

    public static (T min, T max)? Bound<T>(this T[,] matrix) where T : IComparable<T> => matrix.Length == 0
      ? ((T min, T max)?) null
      : matrix.Fold(
        (b, x) => {
          if (x.CompareTo(b.max) > 0) { b.max = x; }
          else if (x.CompareTo(b.min) < 0) { b.min = x; }
          return b;
        },
        InitBound
      );

    public static (T min, T max)?[] BoundRows<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapRows(row => row.Bound());

    public static (T min, T max)?[] BoundColumns<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapColumns(col => col.Bound());
  }
}