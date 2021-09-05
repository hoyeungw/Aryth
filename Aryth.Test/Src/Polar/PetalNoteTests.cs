using Aryth.Polar;
using NUnit.Framework;
using Spare;
using Veho;
using Veho.Enumerable;

namespace Aryth.Test.Polar {
  [TestFixture]
  public partial class PetalNoteTests {
    [Test]
    public void PetalNoteTestAlpha() {
      var petalNote = PetalNote.Build(-36, 5);
      petalNote.Counter.Map(x => (x.Key, x.Value)).DecoEntries().Says("patelNote.Counter");
      petalNote.Marks.Deco().Says("patelNote.Marks");
      var candidates = Seq.From(
        30D, 40, 100, 120, 170, 180, 190, 250, 260, 320, 330, 360
      );
      foreach (var angle in candidates) {
        petalNote.Note(angle).ToString().Says(angle.ToString());
      }
      petalNote.Counter.Map(x => (x.Key, x.Value)).DecoEntries().Says("patelNote.Counter");
    }
  }
}