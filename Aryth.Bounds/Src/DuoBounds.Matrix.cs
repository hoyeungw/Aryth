﻿using Aryth.Bounds.Utils;
using Veho;
using Veho.Columns;
using Veho.Matrix;
using Veho.Rows;

namespace Aryth.Bounds {
  public static partial class DuoBounds {

    public static ((double[,] mat, (double min, double max)?) x, (double[,] mat, (double min, double max)?) y) DuoBound<T>(this T[,] matrix) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Map(x => Assorters.AssortExpandEntryBound(ref bdX, ref bdY, x))
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }

    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBoundRow<T>(this T[,] matrix, int x, int w = 0) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Row(x, v => Assorters.AssortExpandEntryBound(ref bdX, ref bdY, v), w)
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }

    public static ((double[] vec, (double min, double max)?) x, (double[] vec, (double min, double max)?) y) DuoBoundColumn<T>(this T[,] matrix, int y, int h = 0) {
      (double min, double max)? bdX = null;
      (double min, double max)? bdY = null;
      var (veX, veY) = matrix
                       .Column(y, v => Assorters.AssortExpandEntryBound(ref bdX, ref bdY, v), h)
                       .Unwind();
      return ((veX, bdX), (veY, bdY));
    }
  }
}