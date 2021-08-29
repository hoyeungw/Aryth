namespace Aryth {
  public static class FlopperUtil {
    public static T Swap<T>(this T[] vec, int i, int j) {
      var temp = vec[i];
      vec[i] = vec[j];
      return vec[j] = temp;
    }
  }
}