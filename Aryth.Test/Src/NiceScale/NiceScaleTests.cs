using NUnit.Framework;
using Spare;
using Veho;

namespace Aryth.Test.NiceScale {
  [TestFixture]
  public class NiceScaleTests {
    [Test]
    public void Test() {
      var candidates = Vec.From(
        (-0.05, +0.15),
        (0, 1),
        (0, 10),
        (3.14, 9.99),
        (-3.14, 12),
        (13.14, 79.9),
        (14024, 17756),
        (13299, 13304)
      );
      foreach (var valueTuple in candidates) {
        valueTuple.NiceScale().Says(valueTuple.ToString());
      }
    }
  }
}