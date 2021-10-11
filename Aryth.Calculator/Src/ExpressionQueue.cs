using System;
using System.Collections.Generic;
using System.Reflection;
using Aryth.Expression;

namespace Aryth {
  public class ExpressionQueue : Queue<IExpression> {
    public TryParser<string, dynamic> OperandParser;
    public TryParser<string, Type[], MethodInfo> AlgebraParser;

    public static ExpressionQueue Build(TryParser<string, dynamic> operandParser = null,
                                        TryParser<string, Type[], MethodInfo> algebraParser = null) {
      return new ExpressionQueue {
                                   OperandParser = operandParser ?? TryParsers.DefaultOperandParser,
                                   AlgebraParser = algebraParser ?? TryParsers.DefaultMethodParser
                                 };
    }
    public void ParseEnqueue(string text) {
      if (string.IsNullOrEmpty(text)) return;
      if (double.TryParse(text, out var number)) Enqueue(number.ToOperand());
      else if (OperandParser(text, out var value)) this.Enqueue(value.ToOperand());
      else if (AlgebraParser(text, null, out var method)) Enqueue(method.CastOperator());
    }
  }
}