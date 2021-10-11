using System.Collections.Generic;
using Veho;
using static System.Math;

namespace Aryth.Expression {
  public static class NaiveOperand {
    public static readonly Dictionary<string, double> OperandDict = Dict.From(
      Vec.From(
        ("PI", PI),
        ("E", E)
      ));
    public static bool TryParseOperand(string text, out dynamic operand) {
      if (OperandDict.TryGetValue(text, out var result)) {
        operand = result;
        return true;
      }
      operand = null;
      return false;
    }
  }
}