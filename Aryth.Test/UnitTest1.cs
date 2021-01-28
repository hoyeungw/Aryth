using NUnit.Framework;
using Aryth.Bounds;
using Spare.Deco;
using Spare.Logger;
using Veho.Matrix;

namespace Aryth.Test {
  public class Tests {
    [SetUp]
    public void Setup() { }

    [Test]
    public void VectorBoundTest() {
      var vec = new[] {2, 5, 7, 1, 3, 4};
      var result = vec.Bound();
      result.ToString().Logger();
      Assert.Pass();
    }

    [Test]
    public void MatrixBoundTest() {
      var m0 = new[,] {
        {2, 5, 7, 1, 3, 4},
        {3, 6, 8, 2, 7, 1},
        {5, 5, 4, 2, 1, 3},
      };
      m0.Deco().Logger();
      m0.Bound().ToString().Logger();
      m0.BoundRows().ToString().Logger();
      m0.BoundColumns().ToString().Logger();

      var m1 = Inits.Iso<int>(0, 0, default);
      m1.Deco().Logger();
      m1.Bound().ToString().Logger();
      m1.BoundRows().ToString().Logger();
      m1.BoundColumns().ToString().Logger();

      Assert.Pass();
    }
  }
}