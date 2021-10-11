using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Aryth.Expression;
using Ject;
using Veho.Enumerable;
using static Aryth.Expression.NaiveOperator;
using static Aryth.Expression.NaivePlaceholder;

namespace Aryth {
  public class AlgebraCalculator {
    public TryParser<string, dynamic> OperandParser;
    public TryParser<string, Type[], MethodInfo> AlgebraParser;
    public static AlgebraCalculator Build(IDictionary<string, dynamic> operands,
                                          params Type[] algebras) {
      return new AlgebraCalculator {
                                     OperandParser = operands == null ? TryParsers.DefaultOperandParser : operands.TryGetValue,
                                     AlgebraParser = algebras == null ? TryParsers.DefaultMethodParser : TryParserFactory.MakeMethodParser(algebras)
                                   };
    }
    public static AlgebraCalculator Build(TryParser<string, dynamic> operandParser,
                                          params Type[] algebras) {
      return new AlgebraCalculator {
                                     OperandParser = operandParser ?? TryParsers.DefaultOperandParser,
                                     AlgebraParser = algebras == null ? TryParsers.DefaultMethodParser : TryParserFactory.MakeMethodParser(algebras)
                                   };
    }
    public static AlgebraCalculator Build(TryParser<string, dynamic> operandParser = null,
                                          TryParser<string, Type[], MethodInfo> operatorParser = null) {
      return new AlgebraCalculator {
                                     OperandParser = operandParser ?? TryParsers.DefaultOperandParser,
                                     AlgebraParser = operatorParser ?? TryParsers.DefaultMethodParser
                                   };
    }
    public IEnumerable<IExpression> ExpressionToInfix(string text) {
      for (var match = ExpressionRegex.Expression.Match(text); match.Success; match = match.NextMatch()) {
        Group numeric = match.Groups[1], operable = match.Groups[2], alphabetic = match.Groups[3];
        // Debug.Print($" match ({match.Groups[0]}) -> ({numeric})-({operable})-({alphabetic})");
        if (numeric.Success) {
          yield return double.Parse(numeric.Value).ToOperand();
        }
        else if (operable.Success) {
          if (TryParseOperator(operable.Value, out var op)) { yield return op; }
          else if (TryParsePlaceholder(
            operable.Value, out var placeholder)) { yield return placeholder; }
        } // Console.WriteLine($">> [match] {operable.Value} {op.Method?.Name}");
        else if (alphabetic.Success) {
          if (OperandParser(alphabetic.Value, out var operand)) {
            // Debug.Print($">> parse [{alphabetic.Value}] gets {operand}");
            yield return Operand.Build(operand);
          }
          else if (AlgebraParser(alphabetic.Value, null, out var method)) {
            yield return method.CastOperator();
          }
          // Debug.Print($">> parse [{alphabetic.Value}] gets nothing");
        }
      }
    }
    public static Queue<IExpression> InfixToPostfix(IEnumerable<IExpression> infix) {
      var queue = new Queue<IExpression>();
      var stack = new Stack<Operator>();
      infix = infix.ToArray();
      // Debug.Print($">> [infix] {Veho.Sequence.Reducers.Join(infix.ToArray(), x => $"(x)", ", ")}");
      foreach (var expression in infix) {
        switch (expression.Variety) {
          case Variety.Operand:
            queue.Enqueue(expression);
            break;
          case Variety.Void:
          case Variety.Unary:
          case Variety.Binary:
          case Variety.Ternary:
          case Variety.Multiple:
            Operator leftOperator = (Operator)expression, rightOperator;
            while ((
                stack.Any()
              ) && (
                (rightOperator = stack.Peek()).Variety != Variety.LeftBracket
              ) && (
                leftOperator.Precedence < rightOperator.Precedence ||
                rightOperator.Precedence == leftOperator.Precedence && leftOperator.Associativity == Hand.Left
              )
            ) queue.Enqueue(stack.Pop());
            stack.Push(leftOperator);
            break;
          case Variety.LeftBracket:
            stack.Push((Operator)expression);
            break;
          case Variety.RightBracket:
            while (stack.Any() && stack.Peek().Variety != Variety.LeftBracket) queue.Enqueue(stack.Pop());
            stack.Pop();
            break;
        }
        //$"<{idx}> <V Queue> [ {Ξ.hBrief()} ] {(Γ.Any() ? "<O Stack> [ " + Γ.hBrief() + " ]" : "(Φ)")}".wL();
      }
      //$"[Rest stack] {(Γ.Any()? Γ.hBrief(): "(Φ)")}".wL();
      while (stack.Any()) queue.Enqueue(stack.Pop());

      return queue;
    }
    public Operand CalculatePostfix(IEnumerable<IExpression> postfix) {
      var stack = new Stack<Operand>();
      foreach (var expression in postfix) {
        //$"<{idx}> <IExpression> ({it}) <Stack> ({Γ.hBrief()})".wL();
        switch (expression.Variety) {
          case Variety.Operand:
            stack.Push((Operand)expression);
            break;
          case Variety.Void: {
            var @operator = (Operator)expression;
            stack.Push(@operator.Method.StaticInvoke().toOperand());
            break;
          }
          case Variety.Unary: {
            var xOperand = stack.Pop();
            var @operator = AlgebraParser.ReviseOperator((Operator)expression, xOperand.Type);
            stack.Push(@operator.Evaluate(xOperand));
            break;
          }
          case Variety.Binary: {
            var yOperand = stack.Pop();
            var xOperand = stack.Pop();
            var @operator = AlgebraParser.ReviseOperator((Operator)expression, xOperand.Type, yOperand.Type);
            var result = @operator.Evaluate(xOperand, yOperand);
            // Console.WriteLine($">> [operator] {@operator.Value} [x y] {new[] { xOperand.Type, yOperand.Type }.Deco()}");
            // Console.WriteLine($">> {@operator.Name}{new[] { xOperand.Deco(), yOperand.Deco() }.Deco()} = {result.Deco()}");
            stack.Push(result);
            break;
          }
          case Variety.Ternary: {
            var zOperand = stack.Pop();
            var yOperand = stack.Pop();
            var xOperand = stack.Pop();
            var @operator =
              AlgebraParser.ReviseOperator((Operator)expression, xOperand.Type, yOperand.Type, zOperand.Type);
            stack.Push(@operator.Evaluate(xOperand, yOperand, zOperand));
            break;
          }
          case Variety.Multiple: {
            var multipleOperands = stack.PopsIntoArray(((Operator)expression).Length);
            var @operator = AlgebraParser.ReviseOperator((Operator)expression, multipleOperands.Map(x => x.Type));
            stack.Push(@operator.Evaluate(multipleOperands));
            break;
          }
          default:
            break;
        }
        //$"<{idx}> <IExpression> ({it}) <Stack> ({Γ.hBrief()})".wL();
      }
      return stack.Pop();
    }
    public dynamic Calculate(string text) {
      // try {
      var infix = ExpressionToInfix(text);
      var postfix = InfixToPostfix(infix);
      var target = CalculatePostfix(postfix);
      return target.Value;
      // }
      // catch (Exception e) {
      //   Debug.Print($">> calculate error [source] {e.Source}");
      //   Debug.Print($">> calculate error [message] {e.Source}");
      //   throw;
      // }
    }
  }

  public static class CipherCalculator {
    public static dynamic Calculate(this string text, IDictionary<string, dynamic> operands = null,
                                    Type[] algebras = null) {
      try {
        var algebraCalculator = AlgebraCalculator.Build(operands, algebras);
        var infix = algebraCalculator.ExpressionToInfix(text);
        var postfix = AlgebraCalculator.InfixToPostfix(infix);
        var target = algebraCalculator.CalculatePostfix(postfix);
        return target.Value;
      }
      catch (Exception e) {
        // Debug.Print($">> calculate error [source] {e.Source}");
        // Debug.Print($">> calculate error [message] {e.Message}");
        throw;
      }
    }
  }
}