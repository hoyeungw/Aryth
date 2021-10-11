// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using Fdt;
// using Fdt.Reflection;
// using MiscUtil;
// using Veho.Vector;
//
// //Const xem As XElement =
// //<root>
// //    <opr id="pwr" sign="^"    method="op_Exponent"    mode="bin" Associativity="right" Priority="6"/>
// //    <opr id="log" sign="l"    method="Log"            mode="bin" Associativity="right" Priority="6"/>
// //    <opr id="mtp" sign="*"    method="op_Multiply"    mode="bin" Associativity="left" Priority="5"/>
// //    <opr id="div" sign="/"    method="op_Division"    mode="bin" Associativity="left" Priority="5"/>
// //    <opr id="adt" sign="+"    method="op_Addition"    mode="bin" Associativity="left" Priority="4"/>
// //    <opr id="sbt" sign="-"    method="op_Subtraction" mode="bin" Associativity="left" Priority="4"/>
// //    <opr id="max" sign="max"  method="Max"            mode="bin" Associativity="left" Priority="7"/>
// //    <opr id="min" sign="min"  method="Min"            mode="bin" Associativity="left" Priority="7"/>
// //    <opr id="exp" sign="exp"  method="Pow"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="log" sign="log"  method="Log"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="sqr" sign="sqr"  method="Sqr"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="abs" sign="abs"  method="Abs"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="int" sign="int"  method="Int"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="sin" sign="sin"  method="Sin"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="cos" sign="cos"  method="Cos"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="tan" sign="tan"  method="Tan"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="cot" sign="cot"  method="Cot"            mode="una" Associativity="right" Priority="8"/>
// //    <opr id="asn" sign="asin" method="Asin"           mode="una" Associativity="right" Priority="8"/>
// //    <opr id="acs" sign="acos" method="Acos"           mode="una" Associativity="right" Priority="8"/>
// //    <opr id="atn" sign="atan" method="Atan"           mode="una" Associativity="right" Priority="8"/>
// //    <opr id="act" sign="acot" method="Acot"           mode="una" Associativity="right" Priority="8"/>
// //</root>
//
// namespace GaussObsolete {
//   public static class formulaParser {
//     private static Queue<Expression> parseToQueue(string humanExpression, Func<string, Operand> expressionToOperand, Type algebra) {
//       Stack<Operator> Γ = new Stack<Operator>();
//       Queue<Expression> Ξ = new Queue<Expression>();
//       List<string> errMsg = new List<string>();
//       Operator X1 = default(Operator);
//       Operator X2 = default(Operator);
//       string λ = "";
//       void valEnqueueToΞ(ref string cλ) {
//         if (cλ.Length > 0) {
//           Ξ.ParseEnqueue(λ.isNum() ? Convert.ToDouble(λ).toOperand() : expressionToOperand(λ));
//           cλ = "";
//         }
//       }
//       ;
//       void oprEnqueueToΞ(ref string cλ) {
//         X1 = cλ.toOperator(algebra);
//         //Debug.Print Peek(stack)
//         while (Γ.Any() && Γ.Peek().isOperator) {
//           X2 = Γ.Peek();
//           // '"^" is the only right-associative operator
//           if (X1.Associativity == Associativity.left && X1.Priority <= X2.Priority) {
//             Ξ.ParseEnqueue(Γ.Pop());
//           } else if (X1.Associativity == Associativity.right && X1.Priority < X2.Priority) {
//             Ξ.ParseEnqueue(Γ.Pop());
//           } else {
//             break; // TODO: might not be correct. Was : Exit Do
//           }
//         }
//         Γ.Push(X1);
//         //Γ.cW(Function(it) it.token)
//         cλ = "";
//       }
//       ;
//       //Loop over tokens       ' If-loop over tokens (either brackets, operators, or numbers)
//       foreach (char cα in humanExpression) {
//         string α = cα.ToString();
//         //co.w( as String"[α] ({α}) [λ] ({λ}) ")
//         //Ξ.cW(Function(it) it.ToString)
//         switch (α.ToString().ExpressionMode(algebra)) {
//           case ContentType.BinaryOperation:
//             valEnqueueToΞ(ref λ);
//             oprEnqueueToΞ(ref α);
//             break;
//           case ContentType.LeftBracket:
//             if (λ.Length > 0) {
//               if (λ.isOperator(algebra)) {
//                 oprEnqueueToΞ(ref λ);
//               } else {
//                 valEnqueueToΞ(ref λ);
//               }
//             }
//             Γ.Push(α.ToString().toOperator(algebra));
//             break;
//           case ContentType.RightBracket:
//             valEnqueueToΞ(ref λ);
//             while (Γ.Any() && (string)Γ.Peek().Token != "(") {
//               //co.wL( as String"[Γ.Peek()] ({Γ.Peek().token}), [Ξ.First] ({Ξ.First.token}) [Ξ.Last] ({Ξ.Last.token})")
//               Ξ.ParseEnqueue(Γ.Pop());
//             }
//             if (Γ.Count == 0) {
//               errMsg.Add("no matching '('");
//             } else {
//               Γ.Pop();
//               //discard "("
//             }
//             break;
//           case ContentType.Comma:
//             valEnqueueToΞ(ref λ);
//             break;
//           //case ContentType.Space
//           //    valEnqueueToΞ(λ)
//           //    Γ.Push("*".toOperator)
//           default:
//             λ += α;
//             λ = λ.Trim();
//             if (λ == "e") {
//               λ = Math.E.ToString(CultureInfo.CurrentCulture);
//               valEnqueueToΞ(ref λ);
//             } else if (λ == "π") {
//               λ = Math.PI.ToString(CultureInfo.CurrentCulture);
//               valEnqueueToΞ(ref λ);
//             }
//             break;
//           //Ξ.ParseEnqueue(α.toOperand)
//         }
//       }
//       valEnqueueToΞ(ref λ);
//       while (Γ.Any() && !string.IsNullOrEmpty(Γ.Peek().Token.ToString())) {
//         if (string.Equals((string)Γ.Peek().Token, "(", StringComparison.Ordinal))
//           errMsg.Add("no matching ')'");
//         Ξ.ParseEnqueue(Γ.Pop());
//       }
//       //Ξ.cW(Function(it) it.ToString)
//       return Ξ;
//     }
//     private static object Evaluate(Queue<Expression> cΞ, Type algebra) {
//       Stack<Expression> Γ = new Stack<Expression>();
//       while (cΞ.Any()) {
//         Expression V = cΞ.Dequeue();
//         Γ.Push(V);
//         Γ.Pop();
//         Operand X;
//         Operand Y;
//         switch (V.ContentType) {
//           case ContentType.TrinaryOperation:
//             Operand Z = (Operand)Γ.Pop();
//             Y = (Operand)Γ.Pop();
//             X = (Operand)Γ.Pop();
//             Γ.Push(((Operator)V).Evaluate(X, Y, Z));
//             break;
//           case ContentType.BinaryOperation:
//             Y = (Operand)Γ.Pop();
//             X = (Operand)Γ.Pop();
//             Γ.Push(((Operator)V).Evaluate(X, Y));
//             break;
//           case ContentType.UnaryOperation:
//             X = (Operand)Γ.Pop();
//             Γ.Push(((Operator)V).Evaluate(X));
//             break;
//           default:
//             Γ.Push(V);
//             break; //Γ.Push(DirectCast(X, oprExprs).Evaluate(Of mtdTyp)({}))
//         }
//       }
//       return Γ.Pop().Token;
//     }
//     public static tOut Evaluate<tOut>(string txExprs, Func<string, Operand> ofunc, Type algebra) {
//       Queue<Expression> cΞ = parseToQueue(txExprs, ofunc, algebra);
//       object rsl = Evaluate(cΞ, algebra);
//       return (tOut)rsl;
//     }
//     public static double Evaluate(string txExprs) {
//       Queue<Expression> cΞ = parseToQueue(txExprs, it => it.toOperand(), typeof(Math));
//       object rsl = Evaluate(cΞ, typeof(Math));
//       return Convert.ToDouble(rsl);
//     }
//   }
//
//   public class Expression {
//     public object Token { get; set; }
//     public ContentType ContentType { get; set; }
//     public bool isOperator {
//       get {
//         switch (ContentType) {
//           case ContentType.UnaryOperation:
//           case ContentType.BinaryOperation:
//           case ContentType.TrinaryOperation:
//             return true;
//           default:
//             return false;
//         }
//       }
//     }
//     public override string ToString() => Token.ToString();
//   }
//
//   public class Operand : Expression {
//     public Type Type { get; set; }
//   }
//
//   public class Operator : Expression {
//     public string Method { get; set; }
//     public ZOperatorType OperatorType { get; set; }
//     public Associativity Associativity { get; set; }
//     public int Priority { get; set; }
//     public Operand Evaluate(Operand V) {
//       if (typeof(Math).TryParseMethod(this.Method, new[] { V.Type }))
//         return typeof(Math).GetMethod(this.Method, new[] { V.Type })?.Invoke(null, new[] { V.Token })
//                            .toOperand(V.Type);
//       return null;
//     }
//     public Operand Evaluate(Operand L, Operand R) {
//       Func<Operand, Operand, object> fnc = (a, b) => null;
//       if (this.isElementaryArithmetic) {
//         if (L.Type.TryParseMethod(this.Method, new[] { L.Type, R.Type })) {
//           fnc = (a, b) =>
//             a.Type.GetMethod(
//               this.Method, new[] { a.Type, b.Type })?.Invoke(null, new[] { a.Token, b.Token }
//             );
//         } else {
//           switch (this.OperatorType) {
//             case ZOperatorType.adt:
//               fnc = (a, b) => Convert.ToDouble(L.Token) + Convert.ToDouble(R.Token);
//               break;
//             case ZOperatorType.sbt:
//               fnc = (a, b) => Convert.ToDouble(L.Token) - Convert.ToDouble(R.Token);
//               break;
//             case ZOperatorType.mtp:
//               fnc = (a, b) => Convert.ToDouble(L.Token) * Convert.ToDouble(R.Token);
//               break;
//             case ZOperatorType.div:
//               fnc = (a, b) => Convert.ToDouble(L.Token) / Convert.ToDouble(R.Token);
//               break;
//           }
//         }
//       } else {
//         if (typeof(Math).TryParseMethod(this.Method, new[] { L.Type, R.Type })) {
//           fnc = (a, b) => typeof(Math).GetMethod(
//             this.Method, new[] { a.Type, b.Type })?.Invoke(null, new[] { a.Token, b.Token });
//         }
//       }
//       return fnc.Invoke(L, R).toOperand(L.Type);
//     }
//     public Operand Evaluate(params Operand[] vXpAr) {
//       Func<Operand[], object> fnc = vXpS => null;
//       if (vXpAr[0].Type.TryParseMethod(this.Method, vXpAr.Map(it => it.Type)) == true) {
//         fnc = vXpS => vXpAr[0].Type.GetMethod(this.Method, vXpS.Map(it => it.Type))?.Invoke(null, vXpS.Map(it => it.Token));
//       }
//       return fnc.Invoke(vXpAr).toOperand(vXpAr[0].Type);
//     }
//     public bool isElementaryArithmetic => this.OperatorType == ZOperatorType.elementaryArithmeticBinary;
//   }
//
//   public static class RPNExt {
//     private static readonly string[] binSet = { "^", "log", "*", "/", "+", "-", "max", "min" };
//     public static string[,] binInfo = {
//       { "pwr", "Pow", "right", "6" },
//       { "lgn", "Log", "right", "6" },
//       { "mtp", "op_Multiply", "left", "5" },
//       { "div", "op_Division", "left", "5" },
//       { "adt", "op_Addition", "left", "4" },
//       { "sbt", "op_Subtraction", "left", "4" },
//       { "max", "Max", "left", "7" },
//       { "min", "Min", "left", "7" }
//     };
//     public static bool isOperator(this string oprLbl, Type algebra) {
//       if (algebra.TryParseMethod(oprLbl)) return true;
//       if (binSet.Contains(oprLbl)) return true;
//       return false;
//     }
//     public static ContentType ExpressionMode(this string oprLbl, Type algebra) {
//       if (algebra.TryParseMethod(oprLbl)) {
//         switch (algebra.GetMethod(oprLbl).GetParameters().Count()) {
//           case 1:
//             return ContentType.UnaryOperation;
//           case 2:
//             return ContentType.BinaryOperation;
//           case 3:
//             return ContentType.TrinaryOperation;
//           default:
//             return ContentType.Operand;
//         }
//       } else if (oprLbl == "(") {
//         return ContentType.LeftBracket;
//       } else if (oprLbl == ")") {
//         return ContentType.RightBracket;
//       } else if (oprLbl == ",") {
//         return ContentType.Comma;
//       } else if (oprLbl == " ") {
//         return ContentType.Space;
//       } else if (binSet.Contains(oprLbl)) {
//         return ContentType.BinaryOperation;
//       } else {
//         return ContentType.Operand;
//       }
//     }
//     public static Expression toExpression(this string oprLbl) {
//       return new Expression {
//         Token = oprLbl,
//         ContentType = oprLbl.ExpressionMode(typeof(Math))
//       };
//     }
//     public static Operand toOperand<T>(this T oprLbl) {
//       Operand α = new Operand {
//         Token = oprLbl,
//         ContentType = oprLbl.ToString().ExpressionMode(typeof(Math)),
//         Type = typeof(T)
//       };
//       return α;
//     }
//     public static Operand toOperand<T>(this T oprLbl, Type tType) {
//       Operand α = new Operand {
//         Token = oprLbl,
//         ContentType = oprLbl.ToString().ExpressionMode(typeof(Math)),
//         Type = tType
//       };
//       return α;
//     }
//     public static Operator toOperator(this string oprLbl, Type algebra) {
//       ContentType cMode = oprLbl.ExpressionMode(algebra);
//       switch (cMode) {
//         case ContentType.BasicBinaryArithmeticOperation:
//           int idx = Array.FindIndex(binSet, it => it == oprLbl);
//           return new Operator {
//             OperatorType = binInfo[idx, 0].toEnum<ZOperatorType>(),
//             Token = oprLbl,
//             Method = binInfo[idx, 1],
//             Associativity = binInfo[idx, 2].toEnum<Associativity>(),
//             ContentType = cMode,
//             Priority = Convert.ToInt32(binInfo[idx, 3])
//           };
//         case ContentType.UnaryOperation:
//         case ContentType.BinaryOperation:
//         case ContentType.TrinaryOperation:
//           return new Operator {
//             OperatorType = Operator.Convert<ContentType, ZOperatorType>(cMode),
//             Token = oprLbl,
//             Method = oprLbl,
//             Associativity = Associativity.right,
//             ContentType = cMode,
//             Priority = 8
//           };
//         default:
//           return new Operator {
//             OperatorType = ZOperatorType.others,
//             Token = oprLbl,
//             Method = "",
//             Associativity = Associativity.none,
//             ContentType = cMode,
//             Priority = 8
//           };
//       }
//     }
//   }
//
//   public enum ZOperatorType {
//     non = 0,
//     elementaryArithmeticBinary = 0,
//     givenMethodUnary = 1,
//     givenMethodBinary = 2,
//     givenMethodTrinary = 3,
//     adt = 11,
//     sbt = 12,
//     mtp = 13,
//     div = 14,
//     others = 4
//   }
//
//   public enum ContentType {
//     Null = 0,
//     UnaryOperation = 1,
//     BinaryOperation = 2,
//     TrinaryOperation = 3,
//     LeftBracket = 4,
//     RightBracket = 5,
//     Comma = 6,
//     Space = 7,
//     Operand = 10,
//     BasicBinaryArithmeticOperation = 12
//   }
//
//   public enum Associativity {
//     none = 0,
//     left = 1,
//     right = 2
//   }
// }