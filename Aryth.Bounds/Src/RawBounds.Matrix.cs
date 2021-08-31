using System;
using Veho.Columns;
using Veho.Matrix;
using Veho.Rows;

namespace Aryth.Bounds {
  public static partial class RawBounds {


    public static (T min, T max)? Bound<T>(this T[,] matrix) where T : IComparable<T> => matrix.Length == 0
      ? ((T min, T max)?) null
      : matrix.Fold(AssortBound, InitBound);

    public static (T min, T max)?[] BoundRows<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapRows(row => row.Bound());

    public static (T min, T max)?[] BoundColumns<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapColumns(col => col.Bound());
  }
}