namespace Aryth {
  public delegate void ActionByRef<T>(ref T x);

  public delegate bool TryParser<in T, TO>(T x, out TO target);

  public delegate bool TryParser<in TA, in TB, TO>(TA x, TB y, out TO target);

  public delegate void ActionByRef<T1, T2>(ref T1 x, ref T2 y);

  public delegate void IndexedActionByRef<T>(ref T x, int idx);
}