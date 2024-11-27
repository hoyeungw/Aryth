namespace Aryth {
  public static partial class Math {
    // public static bool Has(this (int min, int max) bin, int num, Open open = Open.None) {
    //   var (minOpen, maxOpen) = open.Decode(); // (((byte)open >> 0 & 0b1) == 0b1, ((byte)open >> 1 & 0b1) == 0b1);
    //   return minOpen
    //     ? maxOpen
    //       ? bin.min < num && num < bin.max
    //       : bin.min < num && num <= bin.max
    //     : maxOpen
    //       ? bin.min <= num && num < bin.max
    //       : bin.min <= num && num <= bin.max;
    // }

    /// <summary>for closed</summary>
    public static bool Hold(this (int min, int max) bin, int num) => bin.min <= num && num <= bin.max;

    /// <summary>for open</summary>
    public static bool Allow(this (int min, int max) bin, int num) => bin.min < num && num < bin.max;

    public static int Limit(this (int min, int max) bin, int value) {
      var (min, max) = bin;
      if (value < min) return min;
      if (value > max) return max;
      return value;
    }

    public static int Restrict(this (int min, int max) period, int num) {
      var (min, max) = period;
      var delta = max - min;
      while (num < min) num += delta;
      while (num > max) num -= delta;
      return num;
    }
  }
}