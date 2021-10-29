// using System;
// using System.Linq;
// using Texting;
// using Veho.Vector;
// using static System.Decimal;
//
// namespace Aryth.CipherCalculator.Raw.gauss {
//   public static class Quant {
//     public static T Max<T>(params T[] num) => num.Max();
//     public static T Min<T>(params T[] num) => num.Min();
//   }
//
//   public static class MathExtension {
//     public static bool IsInt(this double v) => System.Math.Abs(v - System.Math.Truncate(v)) < double.Epsilon;
//     public static bool IsInt(this float v) => System.Math.Abs(v - System.Math.Truncate(v)) < float.Epsilon;
//     /// <summary>
//     /// 判断该整数是否是偶数.
//     /// v % 2 == 0.
//     /// </summary>
//     /// <param name="v"></param>
//     /// <returns></returns>
//     public static bool IsEven(this int v) => v % 2 == 0;
//     public static bool IsEven(this long v) => v % 2 == 0;
//     public static int Limit(this int val, int min, int max) => val < max ? val > min             ? val : min : max;
//     public static float Limit(this float val, float min, float max) => val < max ? val > min     ? val : min : max;
//     public static double Limit(this double val, double min, double max) => val < max ? val > min ? val : min : max;
//     public static float[] ProjectTo(this float[] cAr, float iVa, float aVa) {
//       var cΔ = cAr.Max() - cAr.Min();
//       var tΔ = aVa - iVa;
//       var minVal = cAr.Min();
//       var tAr = cAr.Map(it => (it - minVal) / cΔ * tΔ + iVa);
//       return tAr;
//     }
//     public static double[] ProjectTo(this double[] cAr, double iVa, double aVa) {
//       var cΔ = cAr.Max() - cAr.Min();
//       var tΔ = aVa - iVa;
//       var minVal = cAr.Min();
//       var tAr = cAr.Map(it => (it - minVal) / cΔ * tΔ + iVa);
//       return tAr;
//     }
//     // public static float SciIndic(this double v, int decimals = 0) {
//     //   var numTx = v.ToString("E" + (decimals + 1));
//     //   var tIndic = Convert.ToSingle(Strings.Left(numTx, numTx.IndexOf('E') - 1));
//     //   tIndic = (float) Math.Round(tIndic, decimals);
//     //   return tIndic;
//     // }
//     // public static int SciRank(this double v) {
//     //   var tRank = 0;
//     //   if (double.IsNaN(v)) return tRank;
//     //   var numTx = v.ToString("E2");
//     //   tRank = Convert.ToInt32(Strings.Right(numTx, 4));
//     //   return tRank;
//     // }
//     public static (double significand, int exponent) ToSci(this double v, int decimals = 0) {
//       var numTx = v.ToString("E" + decimals);
//       return (significand: Convert.ToSingle(numTx.Pre(numTx.IndexOf('E'))),
//         exponent: Convert.ToInt32(numTx.End(4)));
//     }
//     public static float Significant(this double v, int decimals = 0) =>
//       Convert.ToSingle(v.ToString("E" + decimals).Pre('E'));
//     public static int Exponent(this double v, int decimals = 0) =>
//       Convert.ToInt32(v.ToString("E" + decimals).End(4));
//     public static double Ep(this float v, int exponent) => v * System.Math.Pow(10, exponent);
//     public static double Ep(this double v, int exponent) => v * System.Math.Pow(10, exponent);
//     // public static double[] AutoIntervals(this double[] numArr) {
//     //   (double sig, int exp) sciMax = numArr.Max().ToSci(2);
//     //   (double sig, int exp) sciMin = numArr.Min().ToSci(2);
//     //   (double ceiln, double floor) determinBound((double sig, int exp) max, (double sig, int exp) min) {
//     //     var δRank = Math.Abs(sciMax.exp - sciMin.exp);
//     //     $"[xA] = ({numArr.Max()}), [InticA] = ({sciMax.sig}), [RankA] = ({sciMax.exp}), [signA] = ({(sciMax.sig > 0 ? '+' : '-')})".Wl();
//     //     $"[xI] = ({numArr.Min()}), [InticI] = ({sciMin.sig}), [RankI] = ({sciMin.exp}), [signI] = ({(sciMin.sig > 0 ? '+' : '-')})".Wl();
//     //     $"[ΔRank] = ({δRank})".Wl();
//     //     var ceilnV = δRank > 0 && sciMax.sig < 0 && sciMin.sig < 0
//     //       ? 0
//     //       : sciMax.sig >= 0
//     //         ? sciMax.sig.RoundUp(0)
//     //         : sciMax.sig.RoundDown(0);
//     //     var floorV = δRank > 0 && sciMax.sig > 0 && sciMin.sig > 0
//     //       ? 0
//     //       : sciMin.sig >= 0
//     //         ? sciMin.sig.RoundDown(0)
//     //         : sciMin.sig.RoundUp(0);
//     //     $"[CeilnV] = ({ceilnV}), [FloorV] = ({floorV})".Wl();
//     //     return (ceilnV, floorV);
//     //   }
//     //   ;
//     //   var divisor = 3;
//     //   double uΔ;
//     //   var bound = determinBound(sciMax, sciMin);
//     //   var δv = bound.ceiln * Math.Pow(10, sciMax.exp) - bound.floor * Math.Pow(10, sciMin.exp);
//     //   do {
//     //     divisor++;
//     //     uΔ = (δv / divisor).Significant();
//     //     $"[divisor] = ({divisor}), [uΔ] = ({uΔ}), [ΔV / divisor] = ({δv / divisor}), [isInt(uΔ)] = ({uΔ.IsInt()})".Wl();
//     //   } while (!uΔ.IsInt());
//     //   $"[divisor] = ({divisor}), [ΔV] = ({δv}), [sinItv] = ({uΔ}), [isInt(sinItv)] = ({uΔ.IsInt()})".Wl();
//     //   var δq = δv / divisor;
//     //   var ctI = bound.floor * Math.Pow(10, sciMin.exp);
//     //   return (divisor + 1).IniSqc(ctI, a => a + δq);
//     // }
//     // public static double[] AutoIntervalsBeta(this double[] numArr) {
//     //   var max = numArr.Max();
//     //   var min = numArr.Min();
//     //   //$"[Max] = ({max}); [Min] = ({min})".wL();
//     //   var divLst = new int[] {1, 2, 5};
//     //   (double sig, int exp) sigBaseAdd((double sig, int exp) val, int sigBasePrm) {
//     //     val.sig = val.sig.Ep(sigBasePrm);
//     //     val.exp -= sigBasePrm;
//     //     return val;
//     //   }
//     //   var sigBase = 0;
//     //   int δRank;
//     //   (double sig, int exp) sciMax;
//     //   (double sig, int exp) sciMin;
//     //   do {
//     //     δRank = max.Exponent(sigBase + 2) - min.Exponent(sigBase + 2);
//     //     sciMax = max.ToSci(Math.Max(δRank, sigBase + 2));
//     //     sciMin = min.ToSci(sigBase + 2);
//     //     sciMax = sigBaseAdd(sciMax, sigBase);
//     //     sciMin = sigBaseAdd(sciMin, sigBase);
//     //     sigBase++;
//     //   } while (sciMax.sig.Ep(δRank) - sciMin.sig < 3);
//     //   //$"[sigBase] = ({sigBase}); [ΔRank] = ({ΔRank})".wL();
//     //   //$"[significant] = ({sciMax.sig:N3}), [exponent] = ({sciMax.exp}), [sign] = ({(sciMax.sig > 0 ? '+' : '-')})".wL();
//     //   //$"[significant] = ({sciMin.sig:N3}), [exponent] = ({sciMin.exp}), [sign] = ({(sciMin.sig > 0 ? '+' : '-')})".wL();
//     //   //$"[sciMax.sig.Ep(ΔRank) - sciMin.sig]: ({sciMax.sig.Ep(ΔRank)}) - ({sciMin.sig}) = ({sciMax.sig.Ep(ΔRank) - sciMin.sig})".wL();
//     //   (double ceiln, double floor) determinBound((double sig, int exp) minV, (double sig, int exp) maxV, int div) {
//     //     var ceilnV = δRank > 0 ? maxV.sig.Ep(δRank).ToUpperDiv(div) : maxV.sig.ToUpperDiv(div);
//     //     double floorV = minV.sig.ToLowerDiv(div);
//     //     return (ceilnV, floorV);
//     //   }
//     //   var divIdx = 0;
//     //   double divisor;
//     //   double divCnt;
//     //   do {
//     //     divisor = ((double) divLst[divIdx % 3]).Ep(divIdx / 3);
//     //     var bound = determinBound(sciMin, sciMax, (int) divisor);
//     //     divCnt = (bound.ceiln - bound.floor) / divisor;
//     //     if (divCnt <= 1) {
//     //       sciMax = sigBaseAdd(sciMax, 1);
//     //       sciMin = sigBaseAdd(sciMin, 1);
//     //       divIdx = 0;
//     //     }
//     //     divIdx++;
//     //     //$"[divisor] = ({divisor}); [Range]: [{bound.floor} E({sciMin.exp:+#;-#;0}), {bound.ceiln} E({sciMin.exp:+#;-#;0})];  [divCnt] = ({divCnt})".wL();
//     //   } while (divCnt < 4 | divCnt > 8);
//     //   double[] tArr = Vec.Init((int) divCnt + 1, sciMin.sig.ToLowerDiv((int) divisor), a => a + divisor);
//     //   tArr = tArr.Map(it => it.Ep(sciMin.exp));
//     //   return tArr;
//     // }
//     // public static List<string> VectorToSegment(this double[] numArr) {
//     //   var tLst = new List<string>();
//     //   for (var i = 0; i < numArr.Hi(); i++)
//     //     tLst.Add($"[{numArr[i]}, {numArr[i + 1]})");
//     //   var lastElement = tLst.Last();
//     //   tLst[tLst.Hi()] = lastElement.Splice(lastElement.Hi(), "]");
//     //   return tLst;
//     // }
//   }
//
//   public static class RoundExtension {
//     /// <summary>
//     /// 远离 0 向上舍入
//     /// </summary>
//     public static decimal RoundUp(this decimal v, sbyte digits) {
//       if (digits == 0) return v >= 0 ? Ceiling(v) : Floor(v);
//       var multiple = Convert.ToDecimal(System.Math.Pow(10, digits));
//       return (v >= 0 ? Ceiling(v * multiple) : Floor(v * multiple)) / multiple;
//     }
//     /// <summary>
//     /// 靠近 0 向下舍入
//     /// </summary>
//     public static decimal RoundDown(this decimal v, sbyte digits) {
//       if (digits == 0) return v >= 0 ? Floor(v) : Ceiling(v);
//       var multiple = Convert.ToDecimal(System.Math.Pow(10, digits));
//       return (v >= 0 ? Floor(v * multiple) : Ceiling(v * multiple)) / multiple;
//     }
//     /// <summary>
//     /// 四舍五入
//     /// </summary>
//     public static decimal Round(this decimal v, sbyte digits) {
//       if (digits >= 0) return decimal.Round(v, digits, MidpointRounding.AwayFromZero);
//       var multiple = Convert.ToDecimal(System.Math.Pow(10, -digits));
//       return decimal.Round(v / multiple, MidpointRounding.AwayFromZero) * multiple;
//     }
//     /// <summary>
//     /// 远离 0 向上舍入
//     /// </summary>
//     public static double RoundUp(this double v, sbyte digits) => ToDouble(((decimal)v).RoundUp(digits));
//     /// <summary>
//     /// 靠近 0 向下舍入
//     /// </summary>
//     public static double RoundDown(this double v, sbyte digits) => ToDouble(((decimal)v).RoundDown(digits));
//     /// <summary>
//     /// 四舍五入
//     /// </summary>
//     public static double Round(this double v, sbyte digits) => ToDouble(Round((decimal)v, digits));
//     /// <summary>
//     /// 远离 0 向上舍入
//     /// </summary>
//     public static float RoundUp(this float v, sbyte digits) => ToSingle(((decimal)v).RoundUp(digits));
//     /// <summary>
//     /// 靠近 0 向下舍入
//     /// </summary>
//     public static float RoundDown(this float v, sbyte digits) => ToSingle(((decimal)v).RoundDown(digits));
//     /// <summary>
//     /// 四舍五入
//     /// </summary>
//     public static float Round(this float v, sbyte digits) => ToSingle(Round((decimal)v, digits));
//     public static float ToNear5(this float v) {
//       v = v.RoundDown(0);
//       var mod = v % 5;
//       v -= mod;
//       return mod >= 3 ? v + 5 : v;
//     }
//     public static float ToUpper5(this float v) {
//       var mod = v % 5;
//       v -= mod;
//       return mod > 0 ? v + 5 : v;
//     }
//     public static float ToLower5(this float v) => v - v % 5;
//     public static float ToNearEven(this float v) {
//       var curVal = v.RoundDown(0);
//       return curVal % 2 > 0 ? curVal + 1 : curVal;
//     }
//     public static float ToUpperEven(this float v) {
//       var mod = v % 2;
//       v -= mod;
//       return mod > 0 ? v + 2 : v;
//     }
//     public static float ToLowerEven(this float v) => v - v % 2;
//     public static float ToNearDiv(this float v, int div) {
//       v = v.RoundDown(0);
//       var mod = v % div;
//       v -= mod;
//       return mod >= ((float)div / 2).Round(0) ? v + div : v;
//     }
//     public static float ToUpperDiv(this float v, int div) {
//       var mod = v % div;
//       v -= mod;
//       return mod > 0 ? v + div : v;
//     }
//     public static float ToLowerDiv(this float v, int div) => v - v % div;
//     public static double ToNearDiv(this double v, int div) {
//       v = v.RoundDown(0);
//       var mod = v % div;
//       v -= mod;
//       return mod >= ((float)div / 2).Round(0) ? v + div : v;
//     }
//     public static double ToUpperDiv(this double v, int div) {
//       var mod = v % div;
//       v -= mod;
//       return mod > 0 ? v + div : v;
//     }
//     public static double ToLowerDiv(this double v, int div) {
//       //return v - v % div;
//       var mod = v % div;
//       v -= mod;
//       return mod > 0 ? v : v - div;
//     }
//   }
// }