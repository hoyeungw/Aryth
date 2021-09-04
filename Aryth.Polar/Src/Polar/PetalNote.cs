using System.Collections.Generic;

namespace Aryth.Polar {
  public class PetalNote {
    public List<double> Marks { get; private set; }
    public Dictionary<int, int> Counter { get; private set; }
    
    public int Count => Marks.Count;
    public int Sum { get; private set; }
    public PetalNote Initialize(double startAngle, int count) {
      var unit = 360.0 / count;
      Marks = new List<double>(count);
      Counter = new Dictionary<int, int>(count);
      Sum = 0;
      var angle = Util.Stabilize(startAngle) - unit / 2;
      for (var i = 0; i < count;) {
        Marks.Add(angle);
        Counter.Add(++i, 0);
        angle += unit;
      }
      return this;
    }
    public static PetalNote Build(double startAngle, int count) {
      return new PetalNote().Initialize(startAngle, count);
    }
    public int Phase(double θ) {
      θ = Util.Stabilize(θ);
      for (int i = 0, count = this.Count; i < count; i++) {
        if (θ < Marks[i]) return i == 0 ? count : i;
      }
      return this.Count;
    }
    public (int index, int count) Note(double θ) {
      var phase = this.Phase(θ);
      return (phase, this.NotePhase(phase));
    }
    public int NotePhase(int phase) {
      this.Sum++;
      return Counter[phase] += 1;
    }
  }
}