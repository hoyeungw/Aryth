using System.Reflection;
using Ject;

namespace Aryth {
  public enum Variety {
    None = -1,
    Operand = 0,
    // Operator = 1,
    LeftBracket = 2,
    RightBracket = 3,
    Comma = 4,
    Void = 10,
    Unary = 11,
    Binary = 12,
    Ternary = 13,
    Multiple = 20
  }

  public enum Hand {
    None = -1,
    Left = 1,
    Right = 2
  }

  public static class Utils {
    public static Variety GetVariety(this MethodInfo method) {
      var paramCount = method.ParamTypes().Length;
      switch (paramCount) {
        case 0:  return Variety.Void;
        case 1:  return Variety.Unary;
        case 2:  return Variety.Binary;
        case 3:  return Variety.Ternary;
        default: return Variety.Multiple;
      }
    }
  }
}