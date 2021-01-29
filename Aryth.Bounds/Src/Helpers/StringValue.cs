using Texting.Slices;

namespace Aryth.Bounds.Helpers {
  public static class StringValues {
    public static int V1(this string word) => (word.ToLower()[0] & 0x7f) << 21;
    public static int V2(this string word) => (((word = word.ToLower())[0] & 0x7f) << 21) +
                                              ((word[1] & 0x7f) << 14);
    public static int V3(this string word) => (((word = word.ToLower())[0] & 0x7f) << 21) +
                                              ((word[1] & 0x7f) << 14) +
                                              ((word[2] & 0x7f) << 7);
    public static int V4(this string word) => (((word = word.ToLower())[0] & 0x7f) << 21) +
                                              ((word[1] & 0x7f) << 14) +
                                              ((word[2] & 0x7f) << 7) +
                                              (word[3] & 0x7f);
    public static double StringValue(this string word) {
      var l = word.Length;
      if (l >= 8) return (V4(word.Pre(4)) << 2) + V4(word.Slice(-4));
      if (l == 7) return (V4(word.Pre(4)) << 2) + V3(word.Slice(-3));
      if (l == 6) return (V4(word.Pre(4)) << 2) + V2(word.Slice(-2));
      if (l == 5) return (V4(word.Pre(4)) << 2) + V1(word.Slice(-1));
      if (l == 4) return V4(word) << 2;
      if (l == 3) return V3(word) << 2;
      if (l == 2) return V2(word) << 2;
      if (l == 1) return V1(word) << 2;
      return double.NaN; // include case 0
    }
  }
}