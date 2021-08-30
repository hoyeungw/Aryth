using System.Collections.Generic;
using static System.Math;

namespace Aryth.Polar {
  public static class Graph {
    public const double FoliumAreaOdd = 0.25;
    public const double FoliumAreaEven = 0.5;
    public static List<(double r, double θ)> RhodoneaFolios(this List<(double r, double θ)> list,
                                                            (double r, double θ) rimMark,
                                                            int petals = 3) {
      return list.FindAll(polar => polar.r <= rimMark.FoliateRadius(polar.θ, petals));
    }
    public static List<(double r, double θ)> RhodoneaFolios(this List<(double r, double θ)> list,
                                                            (double r, double θ) rimMark,
                                                            int petals,
                                                            double density) {
      var area = PI * Pow(rimMark.r, 2) * (petals % 2 == 0 ? FoliumAreaEven : FoliumAreaOdd);
      var maximum = (int)Round(density * area);
      var thresholdPerPhase = maximum / petals;
      var petalNote = PetalNote.Build(rimMark.θ, petals);
      var target = new List<(double r, double θ)>(maximum);
      foreach (var polar in list.FiniteFlopper()) {
        if (rimMark.FoliateRadius(polar.θ, petals) < polar.r) continue;
        var phase = petalNote.Phase(polar.θ);
        if (thresholdPerPhase <= petalNote.Counter[phase]) continue;
        petalNote.NotePhase(phase);
        target.Add(polar);
        if (maximum <= petalNote.Sum) break;
      }
      return target;
    }
  }
}