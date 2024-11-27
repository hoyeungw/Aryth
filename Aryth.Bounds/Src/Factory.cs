using System;
using System.Collections.Generic;
using Veho.Columns;
using Veho.Matrix;
using Veho.Rows;
using Veho.Vector;
using static Aryth.Bounds.CoreUtil;

namespace Aryth {
  public static class Factory {
    public static (T min, T max)? Bound<T>(this IReadOnlyList<T> vec) where T : IComparable<T> => vec.Count == 0
      ? ((T min, T max)?)null
      : vec.Fold(AssortBound, InitBound);
    public static (T min, T max)? Bound<T>(this T[,] matrix) where T : IComparable<T> => matrix.Length == 0
      ? ((T min, T max)?)null
      : matrix.Fold(AssortBound, InitBound);

    public static (T min, T max)?[] BoundRows<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapRows(row => row.Bound());

    public static (T min, T max)?[] BoundColumns<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapColumns(col => col.Bound());
  }
}