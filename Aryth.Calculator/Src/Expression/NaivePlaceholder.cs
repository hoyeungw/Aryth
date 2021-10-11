using System.Collections.Generic;
using Veho;

namespace Aryth.Expression {
  public static class NaivePlaceholder {
    public static readonly Dictionary<string, Operator> PlaceholderDict = Dict.From(
      Vec.From(
        ("(", Operator.Build("(", Variety.LeftBracket)),
        (")", Operator.Build(")", Variety.RightBracket)),
        (",", Operator.Build(",", Variety.Comma))
      ));
    public static bool TryParsePlaceholder(string text, out Operator @operator) {
      return PlaceholderDict.TryGetValue(text, out @operator);
    }
  }
}