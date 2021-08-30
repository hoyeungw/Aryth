using System.Collections.Generic;
using NUnit.Framework;
using Veho;

namespace Aryth.Test.Polar {
  [TestFixture]
  public partial class PolarTests {
    public static List<(double x, double y)> Candidates = Seq.From(
      (100.0, 0.0),
      (86.6, 50.0),
      (50.0, 86.6),
      (0.0, 100.0)
    );
  }
}