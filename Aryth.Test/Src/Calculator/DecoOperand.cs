using System;
using Aryth.Expression;

namespace Aryth.Test.Calculator {
  public static class DecoOperand {
    public static string Deco(this Operand operand) {
      Type type = operand.Value.GetType();
      if (type.IsArray) {
        return Spare.Decoes.Deco(operand.Value);
      }
      return operand.ToString();
    }
  }
}