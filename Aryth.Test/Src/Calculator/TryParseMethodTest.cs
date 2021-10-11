using System;
using NUnit.Framework;
using Spare;
using Veho;

namespace Aryth.Test.Calculator {
  [TestFixture]
  public class TryParseMethodTest {
    [Test]
    public void AlphaTest() {
      var a = Vec.From<double>(1, 2, 3);
      var b = 10D;
      var types = new[] { a.GetType(), b.GetType() };
      Console.WriteLine($">> [TryParseGenericMethod] {types.Deco()}");
      if (typeof(LinearSpace<double>).TryParseGenericMethod("Multiply", types, out var method)) {
        Console.WriteLine($">> [method] {method}");
      }
    }
  }
}