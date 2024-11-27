using System;

namespace Aryth {
  public static class Interval {
    public static bool Hold<T>(this (T min, T max) bin, T value) where T : IComparable<T> => bin.min.CompareTo(value) <= 0 && value.CompareTo(bin.max) <= 0;
    public static bool Allow<T>(this (T min, T max) bin, T value) where T : IComparable<T> => bin.min.CompareTo(value) < 0 && value.CompareTo(bin.max) < 0;
  }
}