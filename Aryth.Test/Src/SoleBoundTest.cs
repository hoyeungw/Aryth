﻿using System.Collections.Generic;
using Aryth.Bounds;
using NUnit.Framework;
using Spare.Deco;
using Spare.Logger;
using Veho;

namespace Aryth.Test {
  [TestFixture]
  public class SoleBoundTest {
    [Test]
    public void VectorSoleBoundTest() {
      var candidates = new List<string[]> {
        new[] {"a", "b", "c", "1", "2", "3"},
        new[] {"a", "b", "c"},
        new[] {"1", "2", "3"}
      };
      candidates.Iterate(vec => {
        var (veX, bdX) = vec.SoleBound();
        $"X: [vector] ({veX.Deco()}) [bound] ({bdX})".Logger();
      });
    }

    [Test]
    public void MatrixSoleBoundTest() {
      var candidates = new List<string[,]> {
        new[,] {
          {"a", "b", "c", "1", "2", "3"},
          {"b", "c", "d", "2", "3", "4"},
          {"c", "d", "e", "3", "4", "5"},
          {"d", "e", "f", "4", "5", "6"}
        },
        new[,] {
          {"a", "b", "c"},
          {"b", "c", "d"},
          {"c", "d", "e"},
          {"d", "e", "f"}
        },
        new[,] {
          {"1", "2", "3"},
          {"2", "3", "4"},
          {"3", "4", "5"},
          {"4", "5", "6"}
        },
      };
      candidates.Iterate(vec => {
        var (veX, bdX) = vec.SoleBound();
        $"X: [vector] ({veX.Deco()}) [bound] ({bdX})".LogNext();
      });
    }
  }
}