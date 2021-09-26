using System.Collections.Generic;
using System.Linq;
using static Aryth.Pol;

namespace Aryth.Polar {


  public class PetalNote : IPhaseNote<double, int, int> {
    public List<double> Marks { get; private set; }
    public IDictionary<int, int> Counter { get; private set; }
    public double Epsilon = 0;
    public int Count => Marks.Count;
    public int Sum { get; private set; }
    public static PetalNote Build(double startAngle, int count) {
      return new PetalNote().Initialize(startAngle, count);
    }
    public PetalNote Initialize(double startAngle, int count) {
      var unit = 360.0 / count;
      Epsilon = unit / 2;
      Marks = new List<double>(count);
      Counter = new Dictionary<int, int>(count);
      Sum = 0;
      var angle = LimitDegree(startAngle);
      for (var i = 0; i < count;) {
        Marks.Add(angle);
        Counter.Add(++i, 0);
        angle = LimitDegree(angle + unit); 
      }
      return this;
    }
    public void Clear() {
      Sum = 0;
      for (var i = 0; i < Counter.Count; i++) Counter[Counter.ElementAt(i).Key] = 0;
    }
    public int Phase(double θ) {
      θ = LimitDegree(θ);
      for (int i = 0, count = this.Count; i < count; i++) {
        if (Near(Marks[i], θ, Epsilon)) return i + 1;
      }
      return this.Count;
    }
    public (int phase, int count) Note(double θ) {
      var phase = this.Phase(θ);
      return (phase, this.NotePhase(phase));
    }
    public int NotePhase(int phase) {
      this.Sum++;
      return Counter[phase] += 1;
    }
  }
}