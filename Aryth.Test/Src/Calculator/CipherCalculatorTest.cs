using System;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Calculator {
  [TestFixture]
  public class CipherCalculatorTest {
    [Test]
    public void CipherCalculator() {
      var infixCollection = Seq.From(
        ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
        ("simple", "10.0 + 2 * PI"),
        ("func", "Max(1, 2) + Min(1, 2)"),
        ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )")
        // ("arithNeg", "-5 + 4")
      );
      foreach (var (key, expression) in infixCollection) {
        Console.WriteLine($">> [{key}] {expression}");
        var target = expression.Calculate();
        Console.WriteLine($">> [{key}] {target}");
      }
    }
  }
}