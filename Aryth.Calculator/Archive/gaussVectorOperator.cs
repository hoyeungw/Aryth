// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using MiscUtil;
// using Veho.Columns;
// using Veho.Enumerable;
// using Veho.Matrix;
// using Veho.Rows;
// using Veho.Vector;
// using static System.Math;
//
// namespace Aryth.CipherCalculator.Archive {
//   public static class LinearSpace {
//     #region "array to array"
//     public static T[] And<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_BitwiseAnd(aV, bV);
//     public static T[] Or<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_BitwiseOr(aV, bV);
//     public static T[] Add<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_Addition(aV, bV);
//     public static T[] Minus<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_Subtraction(aV, bV);
//     public static T[] Multiply<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_Multiply(aV, bV);
//     public static T[] Divided<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_Division(aV, bV);
//     public static T[] Concat<T>(this IReadOnlyList<T> aV, IReadOnlyList<T> bV) => VectorZipperOperators.op_Concatenate(aV, bV);
//     #endregion
//
//     #region "array to value"
//     public static T[] And<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_BitwiseAnd(aV, bN);
//     public static T[] Or<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_BitwiseOr(aV, bN);
//     public static T[] Add<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_Addition(aV, bN);
//     public static T[] Minus<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_Subtraction(aV, bN);
//     public static T[] Multiply<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_Multiply(aV, bN);
//     public static T[] Divided<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_Division(aV, bN);
//     public static T[] Concat<T>(this IReadOnlyList<T> aV, T bN) => VectorToValueOperators.op_Concatenate(aV, bN);
//     #endregion
//
//     #region "matrix to matrix"
//     public static T[,] And<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_BitwiseAnd(aX, bX)
//       : MatrixZipperOperators.op_BitwiseAnd(aX, bX);
//     public static T[,] Or<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_BitwiseOr(aX, bX)
//       : MatrixZipperOperators.op_BitwiseOr(aX, bX);
//     public static T[,] Add<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_Addition(aX, bX)
//       : MatrixZipperOperators.op_Addition(aX, bX);
//     public static T[,] Minus<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_Subtraction(aX, bX)
//       : MatrixZipperOperators.op_Subtraction(aX, bX);
//     public static T[,] Multiply<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_Multiply(aX, bX)
//       : MatrixZipperOperators.op_Multiply(aX, bX);
//     public static T[,] Divided<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_Division(aX, bX)
//       : MatrixZipperOperators.op_Division(aX, bX);
//     public static T[,] Concat<T>(this T[,] aX, T[,] bX) => aX.Width() == 1 && bX.Height() == 1
//       ? MatrixLinearOperators.op_Concatenate(aX, bX)
//       : MatrixZipperOperators.op_Concatenate(aX, bX);
//     #endregion
//
//     #region "matrix to value"
//     public static T[,] And<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_BitwiseAnd(aX, bN);
//     public static T[,] Or<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_BitwiseOr(aX, bN);
//     public static T[,] Add<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_Addition(aX, bN);
//     public static T[,] Minus<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_Subtraction(aX, bN);
//     public static T[,] Multiply<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_Multiply(aX, bN);
//     public static T[,] DividedBy<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_Division(aX, bN);
//     public static T[,] Concat<T>(this T[,] aX, T bN) => MatrixToValueOperators.op_Concatenate(aX, bN);
//     #endregion
//   }
//
//   internal static class DimensionalShiftOperators {
//     public static T ZipAsVector<T>(T aVec, T bVec, string methodName) {
//       var method = typeof(VectorZipperOperators)
//                    .GetMethod(methodName)?
//                    .MakeGenericMethod(typeof(T).GetElementType());
//       return (T)method?.Invoke(null, new dynamic[] { aVec, bVec });
//     }
//   }
//
//   internal static class ValueZipperOperators {
//     public static T Concat<T>(T a, T b) => Operator.Convert<string, T>(a.ToString() + b.ToString());
//   }
//
//   internal static class VectorZipperOperators {
//     private static T[] Op<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV, Func<T, T, T> func, MethodBase method) {
//       if (typeof(T).IsArray) func = (a, b) => DimensionalShiftOperators.ZipAsVector<T>(a, b, method.Name);
//       return aV.Zip(bV, func);
//     }
//     public static T[] op_BitwiseAnd<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.And, MethodBase.GetCurrentMethod());
//     public static T[] op_BitwiseOr<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.Or, MethodBase.GetCurrentMethod());
//     public static T[] op_Addition<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.Add, MethodBase.GetCurrentMethod());
//     public static T[] op_Subtraction<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.Subtract, MethodBase.GetCurrentMethod());
//     public static T[] op_Multiply<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.Multiply, MethodBase.GetCurrentMethod());
//     public static T[] op_Division<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) => Op(aV, bV, Operator.Divide, MethodBase.GetCurrentMethod());
//     public static T[] op_Concatenate<T>(IReadOnlyList<T> aV, IReadOnlyList<T> bV) {
//       Func<T, T, T> Factory(Func<T, T, T> defaultFunc, MethodBase method) {
//         if (typeof(T).IsArray) return (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//         if (typeof(T) == typeof(string)) return (a, b) => Operator.Convert<string, T>(a.ToString() + b.ToString());
//         return defaultFunc;
//       }
//       var func = Factory(Operator<T>.Add, MethodBase.GetCurrentMethod());
//       return func.Zipper(aV, bV);
//     }
//   }
//
//   internal static class VectorToValueOperators {
//     private static T[] Op<T>(IReadOnlyList<T> aV, T bN, Func<T, T, T> tfunc, MethodBase method) {
//       if (typeof(T).IsArray) tfunc = (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//       return aV.Map(it => tfunc(it, bN));
//     }
//     public static T[] op_BitwiseAnd<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.And, MethodBase.GetCurrentMethod());
//     public static T[] op_BitwiseOr<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.Or, MethodBase.GetCurrentMethod());
//     public static T[] op_Addition<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.Add, MethodBase.GetCurrentMethod());
//     public static T[] op_Subtraction<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.Subtract, MethodBase.GetCurrentMethod());
//     public static T[] op_Multiply<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.Multiply, MethodBase.GetCurrentMethod());
//     public static T[] op_Division<T>(IReadOnlyList<T> aV, T bN) => Op(aV, bN, Operator.Divide, MethodBase.GetCurrentMethod());
//     public static T[] op_Concatenate<T>(IReadOnlyList<T> aV, T bN) {
//       Func<T, T, T> Factory(Func<T, T, T> defaultFunc, MethodBase method) {
//         if (typeof(T).IsArray) return (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//         if (typeof(T) == typeof(string)) return (a, b) => Operator.Convert<string, T>(a.ToString() + b.ToString());
//         return defaultFunc;
//       }
//       return aV.Map(it => Factory(Operator<T>.Add, MethodBase.GetCurrentMethod())(it, bN));
//     }
//   }
//
//   internal static class MatrixZipperOperators {
//     private static T[,] Op<T>(T[,] aX, T[,] bX, Func<T, T, T> func, MethodBase method) {
//       if (typeof(T).IsArray) func = (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//       return func.Zipper(aX, bX);
//     }
//     public static T[,] op_BitwiseAnd<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.And, MethodBase.GetCurrentMethod());
//     public static T[,] op_BitwiseOr<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Or, MethodBase.GetCurrentMethod());
//     public static T[,] op_Addition<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Add, MethodBase.GetCurrentMethod());
//     public static T[,] op_Subtraction<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Subtract, MethodBase.GetCurrentMethod());
//     public static T[,] op_Multiply<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Multiply, MethodBase.GetCurrentMethod());
//     public static T[,] op_Division<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Divide, MethodBase.GetCurrentMethod());
//     public static T[,] op_Concatenate<T>(T[,] aX, T[,] bX) {
//       Func<T, T, T> Factory(Func<T, T, T> defaultFunc, MethodBase method) {
//         return typeof(T).IsArray      ? (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name) :
//           typeof(T) == typeof(string) ? (a, b) => Operator.Convert<string, T>(a.ToString() + b.ToString()) :
//                                         defaultFunc;
//       }
//       var fn = Factory(Operator<T>.Add, MethodBase.GetCurrentMethod());
//       return fn.Zipper(aX, bX);
//     }
//   }
//
//   internal static class MatrixLinearOperators {
//     public static T[,] Cross<T>(this T[,] aX, T[,] bX, Func<T, T, T> func, int interimIndex = 0) {
//       int height = aX.Height(), width = bX.Width();
//       var matrix = new T[height, width];
//       for (var i = 0; i < height; i++) {
//         for (var j = 0; j < width; j++) {
//           matrix[i, j] = func(aX[i, interimIndex], bX[interimIndex, j]);
//         }
//       }
//       return matrix;
//     }
//     public static TOut[,] Cross<TA, TB, TOut>(this TA[,] aX, TB[,] bX, Func<TA, TB, TOut> func, int interimIndex = 0) {
//       int height = aX.Height(), width = bX.Width();
//       var matrix = new TOut[height, width];
//       for (var i = 0; i < height; i++) {
//         for (var j = 0; j < width; j++) {
//           matrix[i, j] = func(aX[i, interimIndex], bX[interimIndex, j]);
//         }
//       }
//       return matrix;
//     }
//     public static T[,] LinearCross<T>(this T[,] aX, T[,] bX, Func<T, T, T> vectorZipper, Func<T, T, T> sequence) {
//       int height = aX.Height(), width = bX.Width();
//       var matrix = new T[height, width];
//       // var columns=bX.Columns
//       for (var i = 0; i < height; i++) {
//         var row = aX.Row(i);
//         for (var j = 0; j < width; j++) {
//           matrix[i, j] = vectorZipper.Zipper(row, bX.Column(j)).Reduce(sequence);
//         }
//       }
//       // for (var i = 0; i < height; i++)
//       //   for (var j = 0; j < width; j++)
//       //     matrix[i, j] = vectorZipper.Zipper(aX.RowIntoIter(i).ToArray(), bX.ColumnIntoIter(j).ToArray())
//       //                                .Reduce(sequence);
//       return matrix;
//     }
//     // private static T[,]
//     private static T[,] Op<T>(T[,] aX, T[,] bX, Func<T, T, T> func, MethodBase method) {
//       if (typeof(T).IsArray) func = (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//       return aX.Cross<T>(bX, func);
//     }
//     public static T[,] op_BitwiseAnd<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.And, MethodBase.GetCurrentMethod());
//     public static T[,] op_BitwiseOr<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Or, MethodBase.GetCurrentMethod());
//     public static T[,] op_Addition<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Add, MethodBase.GetCurrentMethod());
//     public static T[,] op_Subtraction<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Subtract, MethodBase.GetCurrentMethod());
//     public static T[,] op_Multiply<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Multiply, MethodBase.GetCurrentMethod());
//     public static T[,] op_Division<T>(T[,] aX, T[,] bX) => Op(aX, bX, Operator.Divide, MethodBase.GetCurrentMethod());
//     public static T[,] op_Concatenate<T>(T[,] aX, T[,] bX) {
//       Func<T, T, T> Factory(Func<T, T, T> defaultFunc, MethodBase method) {
//         if (typeof(T).IsArray) return (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//         if (typeof(T) == typeof(string)) return (a, b) => Operator.Convert<string, T>(a.ToString() + b.ToString());
//         return defaultFunc;
//       }
//       return aX.Cross<T>(bX, Factory(Operator<T>.Add, MethodBase.GetCurrentMethod()));
//     }
//   }
//
//   internal static class MatrixToValueOperators {
//     private static T[,] Op<T>(T[,] aX, T bN, Func<T, T, T> func, MethodBase method) {
//       if (typeof(T).IsArray) func = (a, b) => DimensionalShiftOperators.ZipAsVector<T>(a, b, method.Name);
//       return aX.Map(it => func(it, bN));
//     }
//     public static T[,] op_BitwiseAnd<T>(T[,] aX, T bN) => Op(aX, bN, Operator.And, MethodBase.GetCurrentMethod());
//     public static T[,] op_BitwiseOr<T>(T[,] aX, T bN) => Op(aX, bN, Operator.Or, MethodBase.GetCurrentMethod());
//     public static T[,] op_Addition<T>(T[,] aX, T bN) => Op(aX, bN, Operator.Add, MethodBase.GetCurrentMethod());
//     public static T[,] op_Subtraction<T>(T[,] aX, T bN) => Op(aX, bN, Operator.Subtract, MethodBase.GetCurrentMethod());
//     public static T[,] op_Multiply<T>(T[,] aX, T bN) => Op(aX, bN, Operator.Multiply, MethodBase.GetCurrentMethod());
//     public static T[,] op_Division<T>(T[,] aX, T bN) => Op(aX, bN, Operator.Divide, MethodBase.GetCurrentMethod());
//     public static T[,] op_Concatenate<T>(T[,] aX, T bN) {
//       Func<T, T, T> Factory(Func<T, T, T> defaultFunc, MethodBase method) {
//         if (typeof(T).IsArray) return (a, b) => DimensionalShiftOperators.ZipAsVector(a, b, method.Name);
//         if (typeof(T) == typeof(string)) return ValueZipperOperators.Concat;
//         return defaultFunc;
//       }
//       var fn = Factory(Operator<T>.Add, MethodBase.GetCurrentMethod());
//       return aX.Map(it => fn(it, bN));
//     }
//   }
// }