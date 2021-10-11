using System;
using System.Linq;
using NUnit.Framework;
using Spare;
using Veho;
using static Palett.Presets;

namespace Aryth.Test.Calculator.Naive {
  [TestFixture]
  public class CalculatorTest {
    [Test]
    public void NaiveCalculatorTest() {
      var infixCollection = Seq.From(
        ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
        ("simple", "10.0 + 2 * PI"),
        // ("func", "Max(1, 2) + Min(3, 4)"),
        ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )"),
        ("arithNeg", "(-5 + 4)")
      );
      foreach (var (key, value) in infixCollection) {
        Console.WriteLine($">> [key] {value}");
        var infix = NaiveCalculator.ExpressionIntoIter(value).ToList();
        Console.WriteLine($">> [expression -> infix] {infix.Deco(presets: (Metro, Fresh))}");
        var postfix = NaiveCalculator.InfixToPostfix(value);
        Console.WriteLine($">> [postfix] {postfix.Deco(presets: (Metro, Fresh))}");
        Console.WriteLine($">> [calculate] {NaiveCalculator.CalculatePostfix(postfix).ToString("N")}");
        Console.WriteLine($" ");
      }
    }
  }
}