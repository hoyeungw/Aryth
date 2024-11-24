using NUnit.Framework;
using Spare;
using Veho;
using static Aryth.Math;

namespace Aryth.Test.Math {
  [TestFixture]
  public class ScientificNotationTest {
    [Test]
    public void Test() {
      var candidates = Vec.From(3.42522635662055, -0.00002406, 0.00010534437809396, 9735321.58601504, 0, 83.72875, 364.17615);
      foreach (var num in candidates) {
        $"{TrailExpBy(num, 4)}".Says($"{num:0.################}");
      }
    }
  }
}