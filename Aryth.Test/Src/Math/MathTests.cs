using System;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Math {
  [TestFixture]
  public class MathTests {
    [Test]
    public void IntExponTest() {
      var candidates = Vec.From(0.0001, -0.01, 0.1, 0, 10, -100, 1000);
      foreach (var candidate in candidates) {
        Console.WriteLine($">> [{candidate}] {Aryth.Math.IntExp(candidate)}");
      }
    }
  }
}