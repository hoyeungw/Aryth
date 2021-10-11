using System.Text.RegularExpressions;

namespace Aryth {
  public static class ExpressionRegex {
    public const RegexOptions Option = RegexOptions.Compiled | RegexOptions.IgnoreCase;
    public static readonly Regex Numeric = new Regex(@"[+-]?\d+(?:\.\d+)?", Option);
    public static readonly Regex Operator = new Regex(@"[+\-*\/^(),]", Option);
    public static readonly Regex Alphabetic = new Regex(@"[\w\d#$¥%.:|\[\]]+", Option);
    public static readonly Regex Expression = new Regex($@"({Numeric})|({Operator})|({Alphabetic})", Option);
  }
}