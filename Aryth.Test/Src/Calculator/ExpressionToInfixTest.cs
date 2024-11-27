using System;
using System.Collections.Generic;
using Analys;
using Aryth.Archive;
using Aryth.Expression;
using NUnit.Framework;
using Palett;
using Spare;
using Valjoux;
using Veho;
using Veho.Types;

namespace Aryth.Test.Calculator {
  [TestFixture]
  public class ExpressionToInfixTest {
    [Test]
    public void AlphaTest() {
      var infixCollection = Seq.From(
        ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
        ("simple", "10.0 + 2 * PI"), // π
        ("func", "Max(1, 2) + Min(1, 2)"),
        ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )"),
        ("arithNeg", "-5 + 4")
      );
      var algebraCalculator = AlgebraCalculator.Build();
      foreach (var (key, humanExpression) in infixCollection) {
        humanExpression.Says(key + "-expression");
        var infix = algebraCalculator.ExpressionToInfix(humanExpression).ToQueue();
        var postfix = AlgebraCalculator.InfixToPostfix(infix);
        var result = algebraCalculator.CalculatePostfix(postfix);
        infix.ToArray().Deco(delim: " - ", presets: (Presets.Metro, Presets.Fresh)).Says(key + "-infix");
        postfix.ToArray().Deco(delim: " - ", presets: (Presets.Metro, Presets.Fresh)).Says(key + "-postfix");
        result.Says(key + " result");
        Console.WriteLine($"");
      }
    }
  }


  [TestFixture]
  public partial class ExpressionParserTest {
    [Test]
    public void ParserStrategies() {
      var algebraCalculator = AlgebraCalculator.Build();
      var (elapsed, result) = Strategies.Run(
        (int)1E+4,
        Seq.From<(string, Func<string, Queue<IExpression>>)>(
          ("epic", text => NaiveCalculator.ExpressionToInfix(text, x => (IExpression)Operand.Build(x))),
          ("arch", text => ExpressionParser.ExpressionToInfix(text)),
          ("baha", text => new Queue<IExpression>(algebraCalculator.ExpressionToInfix(text)))
        ),
        Seq.From(
          ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
          ("simple", "10.0 + 2 * PI"), // π
          ("func", "Max(1, 2) + Min(1, 2)"),
          ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )"),
          ("arithNeg", "-5 + 4")
        )
      );

      elapsed.Deco(orient: Operated.Rowwise, presets: (Presets.Subtle, Presets.Fresh)).Says("Elapsed");
      // result["alpha", "arch"].Counter.Entries().DecoEntries().Says("alpha x arch");
      // result["alpha", "baha"].Counter.Entries().DecoEntries().Says("alpha x baha");
    }
  }
}