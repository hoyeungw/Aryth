using System;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Flopper {
  [TestFixture]
  public class FlopperTests {
    [Test]
    public void TestAlpha() {
      var vec = Vec.From("foo", "bar", "zen", "des");
      var flopper = FiniteFlopper<string>.From(vec);
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
      Console.WriteLine($">> [next] {flopper.MoveNext()} {flopper.Current}");
    }

    [Test]
    public void TestBeta() {
      var vec = Vec.From("foo", "bar", "zen", "dee");
      var flopper = vec.FiniteFlopper();
      foreach (var s in flopper) {
        Console.WriteLine($">> [next] {s}");
      }
    }
  }
}