using System;
using System.Collections;
using System.Collections.Generic;
using Veho.Vector;

namespace Aryth {
  public class FiniteFlopper<T> : IEnumerator<T> {
    private Random _random;
    private T _curr;
    public T[] Vec { get; private set; }
    public int Length => Vec.Length;
    private int _hi;

    public static FiniteFlopper<T> From(T[] vector) {
      return new FiniteFlopper<T> { Vec = vector, _hi = vector.Length, _random = new Random() };
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
      _hi = Vec.Length;
    }
    public T Current => _curr;
    object IEnumerator.Current => Current;
    public void Dispose() {
      _random = null;
      Vec = null;
      // GC.SuppressFinalize(this);
    }
  }
}