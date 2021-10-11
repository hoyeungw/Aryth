using System;
using System.Linq;
using System.Reflection;
using Ject;
using Veho.Vector;

namespace Aryth.Expression {
  public struct Operator : IExpression {
    public dynamic Value { get; set; }
    public Variety Variety { get; set; }
    public int Precedence { get; set; }
    public Hand Associativity { get; set; }
    public MethodInfo Method => Value is MethodInfo method ? method : null;

    public string Name =>
      Value is MethodInfo method ? method.Name :
      Value is string text       ? text :
                                   null;
    public int Length => Method?.GetParameters().Length ?? 0;
    public Operator ReviseOperator(Type[] paramTypes, params Type[] algebras) {
      var name = Method.Name;
      foreach (var algebra in algebras)
        if (algebra.TryParseMethod(name, paramTypes, out var method)) {
          Value = method;
          return this;
        }
      return this;
    }
    public override string ToString() {
      if (Value is MethodInfo method) {
        // return Typen.Enum.Label(Variety);
        // method.ReturnType;
        return $"{method.Name}";
      }
      return Value.ToString();
    }
    public static Operator Build(dynamic value, Variety variety = Variety.Binary, int precedence = 8, Hand associativity = Hand.None) {
      return new Operator { Value = value, Variety = variety, Precedence = precedence, Associativity = associativity };
    }

    public Operand Evaluate(params Operand[] expressions) {
      var result = Method.StaticInvoke(expressions.Map(it => it.Value));
      return new Operand { Value = result, Variety = Variety.Operand };
    }

    public Operand EvaluateOrRaw(params Operand[] expressions) {
      try {
        var result = Method.StaticInvoke(expressions.Map(it => it.Value));
        return new Operand { Value = result, Variety = Variety.Operand };
      }
      catch (Exception e) {
        // Debug.Print($">> [Error] ({e.HResult}) {e.Message}");
        return expressions.FirstOrDefault();
      }
    }

    public Operand Evaluate<T>(params Operand[] expressions) =>
      Method.StaticInvoke<T>(expressions.Map(it => it.Value)).ToOperand();
  }

  public static class OperatorFactory {
    public static Operator CastOperator(this MethodInfo method) => new Operator {
                                                                                  Value = method,
                                                                                  Variety = method.GetVariety(),
                                                                                  Precedence = 8,
                                                                                  Associativity = Hand.Left
                                                                                };
  }
}