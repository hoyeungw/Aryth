using Aryth.Coord;
using Aryth.Polar;
using Aryth.Polar.Beta;
using NUnit.Framework;
using Spare;
using Veho.List;

namespace Aryth.Test.Polar {
  [TestFixture]
  public partial class PolarTests {
    [Test]
    public void AnalogousTest() {
      var polar = (100.0, 330.0);

      var analogous = polar.Analogous(30, 6);
      analogous.Map(p => (p, p.PolarToCartesian().RoundD1())).DecoEntries().Says("analogous");
    }
  }
}