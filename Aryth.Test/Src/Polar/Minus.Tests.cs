using System;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Polar {
  [TestFixture]
  public class Minus_Tests {
    [Test]
    public void Test() {
      var candidates = Seq.From(
        (180, 90),
        (180, 270),
        (0, 270),
        (0, 90),
        (15, 345),
        (15, 45),
        (15, 135),
        (15, 255)
      );
      foreach (var (a, b) in candidates) {
        Console.WriteLine($">> (a, b) = ({a}, {b}) [distance] {Pol.Distance(a, b)} [minus] {Pol.Minus(a, b)}");
      }
    }
  }
}