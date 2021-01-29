using Aryth.Bounds.Helpers;
using NUnit.Framework;
using Spare.Logger;
using Veho.Vector;

namespace Aryth.Test {
  [TestFixture]
  public class StringValueTests {
    [Test]
    public void StringValueTest() {
      var texts = new[] {
        "Warren",
        "WSJ",
        "GlobalTimes",
        "ZZZZ",
        "zzzz",
        "MetalGear 1",
        "MetalGear 2",
        "123",
        "abc",
        "---",
        "",
      };

      texts.Iterate(x => {
        $"{x}: {x.StringValue()}".Logger();
        // $"{x}: {(x[1] & 0x7f) << 14}".Logger();
      });
    }
  }
}