// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Typen;
// using Veho.Enumerable;
//
// namespace Aryth.CipherCalculator.Raw {
//   public static class GenericFormulaParser {
//     public static T Evaluate<T>(string humanExpression, Func<string, T> parser) {
//       var infix = ExpressionToInfixEpic(humanExpression, parser);
//       var postfix = infix.InfixToPostfix();
//       var operand = postfix.CalculatePostfix<T>(humanExpression);
//       return (T)operand.Value;
//     }
//
//     public static Queue<IExpression> ExpressionToInfixEpic<T>(string humanExpression, Func<string, T> expressionLexicon) {
//       void enqueueIExpression(Queue<IExpression> q, ref string value) {
//         if (value.Any()) {
//           if (value.IsNumeric()) q.ParseEnqueue(value.CastDouble().ToOperand());
//           else if (value.IsOperator<T>()) q.ParseEnqueue(value.MakeOperator<T>());
//           else q.ParseEnqueue(value.ToOperand<T>(expressionLexicon));
//         }
//         value = "";
//       }
//       var queue = new Queue<IExpression>();
//       var λ = "";
//       foreach (var α in humanExpression) {
//         switch (α) {
//           case ' ':
//             enqueueIExpression(queue, ref λ);
//             break;
//           case '.':
//             λ += α;
//             break;
//           case '+':
//           case '-':
//           case '*':
//           case '/':
//           case '^':
//             enqueueIExpression(queue, ref λ);
//             queue.ParseEnqueue(α.MakeOperator());
//             break;
//           case ',':
//             enqueueIExpression(queue, ref λ);
//             queue.ParseEnqueue(new Expression { Value = α, ContentType = ContentType.Comma });
//             break;
//           case '(':
//             enqueueIExpression(queue, ref λ);
//             queue.ParseEnqueue(new Expression { Value = α, ContentType = ContentType.LeftBracket });
//             break;
//           case ')':
//             enqueueIExpression(queue, ref λ);
//             queue.ParseEnqueue(new Expression { Value = α, ContentType = ContentType.RightBracket });
//             break;
//           case 'π':
//             enqueueIExpression(queue, ref λ);
//             queue.ParseEnqueue(new Operand { Value = System.Math.PI, ContentType = ContentType.Operand, Type = System.Math.PI.GetType() });
//             break;
//           default:
//             λ += α;
//             break;
//         }
//       }
//       enqueueIExpression(queue, ref λ);
//       return queue;
//     }
//     public static Operand CalculatePostfix<T>(this IEnumerable<IExpression> postfix, string formula = "") {
//       var stack = new Stack<Operand>();
//       postfix.Iterate(
//         (idx, expression) => {
//           //$"<{idx}> <IExpression> ({it}) <Stack> ({Γ.hBrief()})".wL();
//           switch (expression.ContentType) {
//             case ContentType.Operand: {
//               stack.Push((Operand)expression);
//               break;
//             }
//             case ContentType.Operator:
//               var oper = (Operator)expression;
//               switch (oper.OperatorType) {
//                 case OperatorType.Void: {
//                   stack.Push(oper.ConfigMethod<T>().Method.StaticInvoke().toOperand());
//                   break;
//                 }
//                 case OperatorType.Unary: {
//                   var operand = stack.Pop();
//                   stack.Push(oper.ConfigMethod<T>(operand.Type).Evaluate(operand));
//                   break;
//                 }
//                 case OperatorType.Binary: {
//                   var xOperand = stack.Pop();
//                   var yOperand = stack.Pop();
//                   stack.Push(oper.ConfigMethod<T>(yOperand.Type, xOperand.Type).Evaluate(yOperand, xOperand));
//                   break;
//                 }
//                 case OperatorType.Ternary: {
//                   var zOperand = stack.Pop();
//                   var yOperand = stack.Pop();
//                   var xOperand = stack.Pop();
//                   stack.Push(oper.ConfigMethod<T>(xOperand.Type, yOperand.Type, zOperand.Type).Evaluate(xOperand, yOperand, zOperand));
//                   break;
//                 }
//                 case OperatorType.Multiple: {
//                   var operands = new Operand[oper.Method.GetParameters().Length];
//                   operands.ReverseIterate((ref Operand o) => o = stack.Pop());
//                   stack.Push(oper.ConfigMethod<T>(operands.Map(val => val.Type)).Evaluate(operands));
//                   break;
//                 }
//                 case OperatorType.None:
//                 default:
//                   break;
//               }
//               break;
//             default:
//
//               break;
//           }
//           //$"<{idx}> <IExpression> ({it}) <Stack> ({Γ.hBrief()})".wL();
//         });
//       var result = stack.Pop();
//       result.Name = formula;
//       return result;
//     }
//   }
// }