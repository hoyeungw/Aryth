using Aryth.Bounds.Utils;
using NUnit.Framework;
using Spare;

namespace Aryth.Test {
  [TestFixture]
  public class AssortExpandEntryBoundTest {
    [Test]
    public void Test() {
      (double, double)? bdX = null;
      (double, double)? bdY = null;
      Assorters.AssortExpandEntryBound(ref bdX, ref bdY, 1);
      Assorters.AssortExpandEntryBound(ref bdX, ref bdY, "some");
      Assorters.AssortExpandEntryBound(ref bdX, ref bdY, "ace");
      $"{bdX}, {bdY}".Logger();
      // $"{0} > {double.NaN} = {double.NaN > 0}".Logger();
    }
  }
}