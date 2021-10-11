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
    public void MatrixOperationTest() {
      var infixCollection = Seq.From(
        ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
        ("simple", "Bar[b] * 10.0 / Foo[a] * 1"),
        ("func", "Max(1, 2) + Min(1, 2)"),
        ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )"),
        ("( a + b ) / b", "( Foo[a] + Bar[b] ) / Bar[b]"),
        ("c + d", "Zen[c] + Rio_d")
        // ("arithNeg", "- 5 + 4")
      );

      var a = new double[,] {
                              { 1, 2, 3 },
                              { 4, 5, 6 },
                              { 7, 8, 9 }
                            };
      var b = new double[,] {
                              { 1, 0, 0 },
                              { 0, 1, 0 },
                              { 0, 0, 1 }
                            };
      var c = new double[,] {
                              { 1, double.NaN, 2 },
                              { 1, double.NaN, 2 },
                              { 1, double.NaN, 2 }
                            };
      var d = new double[,] {
                              { double.PositiveInfinity, 0, 0, },
                              { 0, double.NaN, 0 },
                              { 0, 0, double.NegativeInfinity, }
                            };

      var operands = Seq.From<(string, dynamic)>(
        ("PI", System.Math.PI),
        ("Foo[a]", (dynamic)a),
        ("Bar[b]", (dynamic)b),
        ("Zen[c]", (dynamic)c),
        ("Rio_d", (dynamic)d)
      ).ToDict();

      AlgebraCalculator algebraCalculator = AlgebraCalculator.Build(operands, typeof(LinearSpace<double>), typeof(GenericMath), typeof(System.Math), typeof(double));
      foreach (var (key, value) in infixCollection) {
        Console.WriteLine($">> [{key}] {value}");
        var queue = algebraCalculator.ExpressionToInfix(value).ToQueue();
        Console.WriteLine($">> [key] {queue.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
        var postfix = AlgebraCalculator.InfixToPostfix(queue);
        Console.WriteLine($">> [key] {postfix.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
        var result = algebraCalculator.CalculatePostfix(postfix);
        var body = result.Value;
        if (body is double[,] matrix) {
          Console.WriteLine($">> [operand] {(matrix).Deco()}");
        }
        else if (body is double[] vector) {
          Console.WriteLine($">> [operand] {(vector).Deco()}");
        }
        else {
          Console.WriteLine($">> [operand] {result.Deco()}");
        }
      }
    }
  }
}