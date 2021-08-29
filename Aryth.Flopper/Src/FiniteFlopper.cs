using System;
using System.Collections;
using System.Collections.Generic;

namespace Aryth {
  public class FiniteFlopper<T> : IEnumerator<T> {
    private T[] _vector;
    private Random _random;
    private T _curr;
    public T[] Vec => _vector;
    public int Length => _vector.Length;
    private int _hi;

    public static FiniteFlopper<T> From(T[] vector) {
      return new FiniteFlopper<T> { _vector = vector, _hi = vector.Length, _random = new Random() };
    }
    public IEnumerable<T> Next {
      get {
        var l = Length;
        while (--l >= 0) yield return Vec.Swap(_random.Next(l), l);
      }
    }
    public bool MoveNext() {
      while (--_hi >= 0) {
        _curr = Vec.Swap(_random.Next(_hi), _hi);
        return true;
      }
      return false;
    }
    public void Reset() {
      _hi = _vector.Length;
    }
    public T Current => _curr;
    object IEnumerator.Current => Current;
    public void Dispose() {
      _random = null;
      _vector = null;
      // GC.SuppressFinalize(this);
    }
  }
}