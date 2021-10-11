using NUnit.Framework;
using Spare;
using Veho;

namespace Aryth.Test.Calculator {
  [TestFixture]
  public class LinearSpace {
    [Test]
    public void VectorCalculationTest() {
      var alpha = (3, 3).Init((i, j) => i);
      var beta = (3, 3).Init((i, j) => j);
      var gamma = alpha.op_Addition(beta);


      gamma.Deco().Says("gamma");
    }
  }
}