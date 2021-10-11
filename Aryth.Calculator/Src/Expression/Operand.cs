using System;

namespace Aryth.Expression {
  public struct Operand : IExpression {
    public dynamic Value { get; set; }
    public Variety Variety { get; set; }

    public Type Type => Value.GetType();

    public override string ToString() => Value.ToString();

    public string Deco() {
      Type type = Value.GetType();
      if (type.IsArray) {
        return "[ " + string.Join(", ", Value) + " ]";
      }
      return ToString();
    }

    public static Operand Build(dynamic value) => new Operand { Value = value, Variety = Variety.Operand };
  }

  internal static class OperandFactory {
    public static Operand ToOperand<T>(this T operand) => new Operand {
                                                                        Value = operand,
                                                                        Variety = Variety.Operand
                                                                      };
  }
}