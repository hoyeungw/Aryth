using System;
using System.Collections.Generic;
using Veho;
using Veho.Formula;

namespace Aryth.Expression {
  public static class NaiveOperator {
    public static readonly Dictionary<string, Operator> OperatorDict = Dict.From(
      Vec.From(
        ("^", Operator.Build(new Func<double, double, double>(Algebra.op_Concatenate).Method, Variety.Binary, 6, Hand.Right)),
        ("*", Operator.Build(new Func<double, double, double>(Algebra.op_Multiply).Method, Variety.Binary, 5, Hand.Left)),
        ("/", Operator.Build(new Func<double, double, double>(Algebra.op_Division).Method, Variety.Binary, 5, Hand.Left)),
        ("+", Operator.Build(new Func<double, double, double>(Algebra.op_Addition).Method, Variety.Binary, 4, Hand.Left)),
        ("-", Operator.Build(new Func<double, double, double>(Algebra.op_Subtraction).Method, Variety.Binary, 4, Hand.Left))
      ));

    public static bool TryParseOperator(string text, out Operator @operator) {
      return OperatorDict.TryGetValue(text, out @operator);
    }
  }
}