using System;
using System.Collections.Generic;
using System.Linq;
using Analys;
using Aryth.Test.Polar.Aryth.Polar;
using NUnit.Framework;
using Palett;
using Spare;
using Valjoux;
using Veho;
using Veho.Sequence;
using Veho.Types;
using static Aryth.Pol;

namespace Aryth.Test.Polar {
  namespace Aryth.Polar {
    public class PetalNoteAlpha : IPhaseNote<double, int, int> {
      public List<double> Marks { get; private set; }
      public IDictionary<int, int> Counter { get; private set; }
      public int Count => Marks.Count;
      public int Sum { get; private set; }
      public PetalNoteAlpha Initialize(double startAngle, int count) {
        var unit = 360.0 / count;
        Marks = new List<double>(count);
        Counter = new Dictionary<int, int>(count);
        Sum = 0;
        var angle = Restrict(startAngle) - unit / 2;
        for (var i = 0; i < count;) {
          Marks.Add(angle);
          Counter.Add(++i, 0);
          angle = Restrict(angle + unit);
        }
        return this;
      }
      public void Clear() {
        Sum = 0;
        for (var i = 0; i < Counter.Count; i++) Counter[Counter.ElementAt(i).Key] = 0;
      }
      public int Phase(double θ) {
        θ = Restrict(θ);
        var prev = Marks.First();
        for (int i = 1, count = Count; i <= count; i++) {
          var next = Marks[i % count];
          if ((prev, next).Contains(θ)) return i;
          prev = next;
        }
        return Count;
      }
      public (int phase, int count) Note(double θ) {
        var phase = Phase(θ);
        return (phase, NotePhase(phase));
      }
      public int NotePhase(int phase) {
        Sum++;
        return Counter[phase] += 1;
      }
    }

    public class PetalNoteBeta : IPhaseNote<double, int, int> {
      public List<double> Marks { get; private set; }
      public IDictionary<int, int> Counter { get; private set; }
      public double Epsilon = 0;
      public int Count => Marks.Count;
      public int Sum { get; private set; }
      public PetalNoteBeta Initialize(double startAngle, int count) {
        var unit = 360.0 / count;
        Epsilon = unit / 2;
        Marks = new List<double>(count);
        Counter = new Dictionary<int, int>(count);
        Sum = 0;
        var angle = Restrict(startAngle);
        for (var i = 0; i < count;) {
          Marks.Add(angle);
          Counter.Add(++i, 0);
          angle = Restrict(angle + unit);
        }
        return this;
      }
      public void Clear() {
        Sum = 0;
        for (var i = 0; i < Counter.Count; i++) Counter[Counter.ElementAt(i).Key] = 0;
      }
      public int Phase(double θ) {
        θ = Restrict(θ);
        for (int i = 0, count = Count; i < count; i++) {
          if (Near(Marks[i], θ, Epsilon)) return i + 1;
        }
        return Count;
      }
      public (int phase, int count) Note(double θ) {
        var phase = Phase(θ);
        return (phase, NotePhase(phase));
      }
      public int NotePhase(int phase) {
        Sum++;
        return Counter[phase] += 1;
      }
    }
  }

  [TestFixture]
  public partial class PetalNoteTests {
    [Ignore("ignore PetalNoteTests")]
    [Test]
    public void PetalNoteStrategies() {
      var arch = new PetalNoteAlpha().Initialize(36, 5);
      var baha = new PetalNoteBeta().Initialize(36, 5);

      // var candidates = Seq.From(30D, 40, 100, 120, 170, 180, 190, 250, 260, 320, 330, 360);
      // candidates.Iterate(x => baha.Note(x));
      // baha.Clear();
      // baha.Counter.Entries().DecoEntries().Says("baha");

      var (elapsed, result) = Strategies.Run(
        (int)1E+5,
        Seq.From<(string, Func<List<double>, IPhaseNote<double, int, int>>)>(
          ("arch", vec => {
            arch.Clear();
            // Console.WriteLine($">> [arch] {arch.Sum}");
            // Console.WriteLine($">> [arch] {arch.Marks.Deco()}");
            vec.IterateList(x => arch.Note(x));
            return arch;
          }),
          ("baha", vec => {
            baha.Clear();
            // Console.WriteLine($">> [baha] {baha.Sum}");
            // Console.WriteLine($">> [arch] {baha.Marks.Deco()}");
            vec.IterateList(x => baha.Note(x));
            return baha;
          })
        ),
        Seq.From(
          ("alpha", Seq.From(30D, 40, 100, 120, 170, 180, 190, 250, 260, 320, 330, 360))
          // ("beta", Seq.From(30D, 40, 100, 120, 170, 180, 190, 250, 260, 320, 330, 360))
        )
      );

      elapsed.Deco(orient: Operated.Rowwise, presets: (Presets.Subtle, Presets.Fresh)).Says("Elapsed");
      result["alpha", "arch"].Counter.Entries().DecoEntries().Says("alpha x arch");
      result["alpha", "baha"].Counter.Entries().DecoEntries().Says("alpha x baha");
    }
  }
}