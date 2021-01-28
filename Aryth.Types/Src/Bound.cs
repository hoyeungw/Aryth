namespace Aryth.Types {
  public class Bound<T> {
    public static Bound<T> From(T min, T max) => new Bound<T> {Max = max, Min = min};
    public T Max;
    public T Min;
  }
}