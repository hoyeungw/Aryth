using System;
using System.Reflection;
using Aryth.Expression;
using Ject;

namespace Aryth {
  public static class TryParsers {
    public static readonly Type[] DefaultAlgebras = { typeof(System.Math), typeof(Math), typeof(double) };
    public static readonly TryParser<string, Type[], MethodInfo> DefaultMethodParser = (string methodName, Type[] types, out MethodInfo method) => {
      foreach (var algebra in DefaultAlgebras)
        if (algebra.TryParseMethod(methodName, types, out method))
          return true;
      method = null;
      return false;
    };
    public static readonly TryParser<string, dynamic> DefaultOperandParser = (string operandName, out dynamic operand) => NaiveOperand.TryParseOperand(operandName, out operand);
    public static Operator ReviseOperator(this TryParser<string, Type[], MethodInfo> operatorParser, Operator @operator, params Type[] paramTypes) {
      // Console.WriteLine($">> revising [method] {@operator.Name} {paramTypes?.Deco()}");
      if (operatorParser(@operator.Name, paramTypes, out var method)) {
        // Console.WriteLine($">> [revised] {method}");
        @operator.Value = method;
        return @operator;
      }
      return @operator;
    }
  }

  public static class TryParserFactory {
    // public static readonly Dictionary<string, string> Alias = new Dictionary<string, string> {
    //                                                                                            { "Multiply", "op_Multiply" },
    //                                                                                            { "Divide", "op_Division" },
    //                                                                                            { "Add", "op_Addition" },
    //                                                                                            { "Subtract", "op_Subtraction" },
    //                                                                                          };
    public static TryParser<string, Type[], MethodInfo> MakeMethodParser(params Type[] algebraCollection) {
      return (string methodName, Type[] paramTypes, out MethodInfo method) => {
        foreach (var algebra in algebraCollection) {
          // Console.WriteLine($">> searching [algebra] ({algebra}) for [method] {methodName}{paramTypes?.Deco()}");
          if (algebra.TryParseMethod(methodName, paramTypes, out method) || algebra.TryParseGenericMethod(methodName, paramTypes, out method)) {
            // Console.WriteLine($">> TryParseMethod [{algebra}] {methodName} {paramTypes} {method}");
            return true;
          }
        }
        method = null;
        return false;
      };
    }
  }
}