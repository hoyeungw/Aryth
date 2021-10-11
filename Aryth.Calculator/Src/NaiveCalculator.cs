using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Math;

namespace Aryth {
  public static class NaiveCalculator {
    public static IEnumerable<Match> ExpressionIntoIter(string expression) {
      return ExpressionRegex.Expression.Matches(expression).Cast<Match>();
    }
    public static Queue<T> ExpressionToInfix<T>(string expression, Func<string, T> func) {
      var queue = new Queue<T>();
      foreach (var match in ExpressionIntoIter(expression)) {
        if (match.Success) queue.Enqueue(func(match.Value));
      }
      return queue;
    }
    public static readonly Dictionary<string, (byte precedence, Hand associativity)> Operators =
      new Dictionary<string, (byte, Hand)> {
                                             { "^", (4, Hand.Right) },
                                             { "*", (3, Hand.Left) },
                                             { "/", (3, Hand.Left) },
                                             { "%", (3, Hand.Left) },
                                             { "+", (2, Hand.Left) },
                                             { "-", (2, Hand.Left) }
                                           };
    public static readonly Dictionary<string, double> Constants =
      new Dictionary<string, double> {
                                       { "E", E },
                                       { "PI", PI },
                                       // {"LN10", Math.LN10},
                                       // {"LN2", Math.LN2},
                                       // {"LOG2E", Math.LOG2E},
                                       // {"LOG10E", Math.LOG10E},
                                       // {"SQRT1_2", Math.SQRT1_2},
                                       // {"SQRT2", Math.SQRT2}
                                     };
    public static readonly Dictionary<string, Func<double, double>> Functions =
      new Dictionary<string, Func<double, double>> {
                                                     { "Abs", Abs },
                                                     { "Exp", Exp },
                                                     { "Round", Round },
                                                     { "Sin", Sin },
                                                     { "Cos", Cos },
                                                     { "Tan", Tan },
                                                   };

    private static readonly Action ThrowNoLeftParenth = () => throw new ArgumentException("No matching left parenthesis.");
    public static List<dynamic> InfixToPostfix(string infix,
                                               Dictionary<string, Func<double, double>> functions = null,
                                               Dictionary<string, double> constants = null) {
      constants = constants ?? Constants;
      functions = functions ?? Functions;
      string xOp, yOp;
      (byte precedence, Hand associativity) xInfo, yInfo;
      var postfix = new List<dynamic>();
      var stack = new Stack<string>();
      foreach (var match in ExpressionIntoIter(infix)) {
        // Console.WriteLine($">> [match] {match}");
        var text = match.Value;
        if (double.TryParse(text, out var number)) { postfix.Add(number); }
        else if (constants.TryGetValue(text, out var constant)) { postfix.Add(constant); }
        else if (functions.ContainsKey(text)) { stack.Push(text); }
        else if (text == ",") { postfix.AddRange(stack.PopUntil(x => x == "(", ThrowNoLeftParenth)); }
        else if (Operators.TryGetValue(text, out xInfo)) {
          xOp = text;
          yOp = stack.Count > 0 ? stack.Peek() : "";
          // Console.WriteLine($">> [xOp] {xOp} [yOp] {yOp}");
          while (Operators.TryGetValue(yOp, out yInfo) && (
            (xInfo.associativity == Hand.Left && (xInfo.precedence <= yInfo.precedence)) ||
            (xInfo.associativity == Hand.Right && (xInfo.precedence < yInfo.precedence))
          )) {
            postfix.Add(yOp);
            stack.Pop();
            yOp = stack.Peek();
          }
          stack.Push(xOp);
        }
        else if (text == "(") { stack.Push(text); }
        else if (text == ")") { postfix.AddRange(stack.PopUntil(x => x == "(", ThrowNoLeftParenth)); }
        // Console.WriteLine($">> [stack] {string.Join(", ", stack.ToArray())} [postfix] {string.Join(", ", postfix.ToArray())}");
      }
      // Console.WriteLine($">> [stack count] {stack.Count}");
      while (stack.Count > 0) {
        var top = stack.Pop();
        if (top == "(" || top == ")") throw new ArgumentException("No matching right parenthesis.");
        postfix.Add(top);
      }
      return postfix;
    }


    public static double CalculatePostfix(List<dynamic> postfix) {
      var stack = new Stack<double>();
      foreach (var value in postfix) {
        if (value is double) { stack.Push(value); }
        else if (value is Func<double, double>) { stack.Push(value(stack.Pop())); }
        else {
          double yOperand = stack.Pop(), xOperand = stack.Pop();
          // Console.Write($">> {o1} {value} {o2} ");
          if (value == "+") { stack.Push(xOperand + yOperand); }
          else if (value == "-") { stack.Push(xOperand - yOperand); }
          else if (value == "*") { stack.Push(xOperand * yOperand); }
          else if (value == "/") { stack.Push(xOperand / yOperand); }
          else if (value == "^") { stack.Push(Pow(xOperand, yOperand)); }
          // Console.WriteLine($" = {stack.Peek()}");
        }
      }
      return stack.Pop();
    }
  }
}