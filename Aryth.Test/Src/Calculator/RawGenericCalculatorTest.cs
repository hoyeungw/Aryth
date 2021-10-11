// using System;
// using System.Linq;
// using Aryth.CipherCalculator.Raw;
// using NUnit.Framework;
// using Palett;
// using Spare;
// using Veho;
//
// namespace Aryth.Test.CipherCalculator {
//   [TestFixture]
//   public class RawGenericCalculatorTest {
//     [Test]
//     public void VectorCalculationTest() {
//       var infixCollection = Seq.From(
//         ("standard", "3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3"),
//         ("simple", "10.0 + 2 * π"),
//         ("func", "max(1, 2) + min(1, 2)"),
//         ("parenth", "( ( 1 + 2 ) ^ ( 3 + 4 ) ) ^ ( 5 + 6 )")
//         // ("arithNeg", "- 5 + 4")
//       );
//       foreach (var (key, value) in infixCollection) {
//         Console.WriteLine($">> [{key}] {value}");
//         var queue = CipherCalculator.ExpressionToInfixEpic(value);
//         Console.WriteLine($">> [key] {queue.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
//         var postFix = queue.InfixToPostfix();
//         Console.WriteLine($">> [key] {postFix.ToList().Deco(presets: (Presets.Metro, Presets.Fresh))}");
//         var result = postFix.CalculatePostfix();
//         Console.WriteLine($">> [operand] {result}");
//       }
//     }
//   }
// }