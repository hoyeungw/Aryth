﻿using System;
using Veho.Matrix;
using Veho.Matrix.Columns;
using Veho.Matrix.Rows;
using Veho.Vector;

namespace Aryth.Bounds {
  public static class Bounders {
    private static (T min, T max) InitBound<T>(T value) => (value, value);
    private static (T min, T max) PileBound<T>((T min, T max) bound, T value) where T : IComparable<T> {
      if (value.CompareTo(bound.max) > 0) { bound.max = value; }
      else if (value.CompareTo(bound.min) < 0) { bound.min = value; }
      return bound;
    }
    public static (T min, T max)? Bound<T>(this T[] vec) where T : IComparable<T> => vec.Length == 0
      ? ((T min, T max)?) null
      : vec.Fold(PileBound, InitBound);

    public static (T min, T max)? Bound<T>(this T[,] matrix) where T : IComparable<T> => matrix.Length == 0
      ? ((T min, T max)?) null
      : matrix.Fold(PileBound, InitBound);

    public static (T min, T max)?[] BoundRows<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapRows(row => row.Bound());

    public static (T min, T max)?[] BoundColumns<T>(this T[,] matrix) where T : IComparable<T> => !matrix.Any()
      ? Array.Empty<(T, T)?>()
      : matrix.MapColumns(col => col.Bound());
  }
}