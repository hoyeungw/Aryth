using System;

namespace Aryth {
  [Flags]
  public enum Open {
    None = 0b00,
    Min = 0b01,
    Max = 0b10,
  }

  public static class EnumUtil {
    public static (bool min, bool max) Decode(this Open open) => (((byte)open >> 0 & 0b1) == 0b1, ((byte)open >> 1 & 0b1) == 0b1);

    // public static bool Equals<T>(this (T x, T y) a, (T x, T y) b) where T : IComparable<T> => a.x.CompareTo(b.x) == 0 && a.y.CompareTo(b.y) == 0;
    // public static bool Hold<T>(this (T min, T max) bin, T value) where T : IComparable<T> => bin.min.CompareTo(value) <= 0 && value.CompareTo(bin.max) <= 0;
    // public static bool Allow<T>(this (T min, T max) bin, T value) where T : IComparable<T> => bin.min.CompareTo(value) < 0 && value.CompareTo(bin.max) < 0;

    public static string ToStr<T>(this (T min, T max) bin, Open open = Open.None) {
      var c = open.Decode();
      var a = c.min ? '(' : '[';
      var b = c.max ? ')' : ']';
      return $"{a}{bin.min}, {bin.max}{b}";
    }
  }
}