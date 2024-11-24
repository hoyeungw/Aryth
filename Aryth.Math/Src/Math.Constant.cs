using System.Text.RegularExpressions;

namespace Aryth {
  public static partial class Math {
    private static Regex TrailRegex { get; } = new Regex(@"\-?(\d+)E(\-?\d+)");
    public static string SciNote { get; set; } = "0.000000E0";

    private const float E2 = 100;
    private const float E3 = 1000;
    private const float E4 = 10000;
  }
}