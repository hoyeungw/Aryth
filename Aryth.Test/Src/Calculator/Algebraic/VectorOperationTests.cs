using System;
using System.Linq;
using Generic.Math;
using NUnit.Framework;
using Palett;
using Spare;
using Veho;

namespace Aryth.Test.Calculator.Algebraic {
  [TestFixture]
  public partial class AlgebraicTests {
    [Test]
    public void VectorCalculationTest() {
      var infixCollection = Seq.From(
        ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
        ("simple", "Bar[a] * 10.0 / Rio_C * 1"),
        ("func", "Max(1, 2) + Min(1, 2)"),
        ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )")
        // ("arithNeg", "- 5 + 4")
      );

      var operands = Seq.From<(string, dynamic)>(
        ("PI", System.Math.PI),
        ("Foo", (dynamic)Vec.From<double>(1, 2, 3)),
        ("Bar[a]", (dynamic)Vec.From<double>(12, 12, 12)),
        ("Zen", (dynamic)Vec.From<double>(1, 1, 1)),
        ("Rio_C", (dynamic)Vec.From<double>(1, 2, 3))
      ).ToDict();

      AlgebraCalculator algebraCalculator = AlgebraCalculator.Build(operands, typeof(LinearSpace<double>), typeof(GenericMath), typeof(System.Math), typeof(double));
      foreach (var (key, value) in infixCollection) {
        Console.WriteLine($">> [{key}] {value}");
        var queue = algebraCalculator.ExpressionToInfix(value).ToQueue();
        Console.WriteLine($">> [key] {queue.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
        var postfix = AlgebraCalculator.InfixToPostfix(queue);
        Console.WriteLine($">> [key] {postfix.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
        var result = algebraCalculator.CalculatePostfix(postfix);
        Console.WriteLine($">> [result] {result.Deco()}");
      }
    }
  }
}