using System;
using System.Collections.Generic;

namespace Aryth {
  public static class IteratorUtil {
    public static Queue<T> ToQueue<T>(this IEnumerable<T> iter) {
      var queue = new Queue<T>();
      foreach (var x in iter) { queue.Enqueue(x); }
      return queue;
    }

    public static Stack<T> ToStack<T>(this IEnumerable<T> iter) {
      var stack = new Stack<T>();
      foreach (var x in iter) { stack.Push(x); }
      return stack;
    }
    public static T[] PopsIntoArray<T>(this Stack<T> stack, int count) {
      var vec = new T[count];
      var i = count;
      while (i > 0) { vec[--i] = stack.Pop(); }
      return vec;
    }

    public static IEnumerable<T> PopUntil<T>(this Stack<T> stack, Func<T, bool> term, Action otherwise) {
      T top = default;
      while (stack.Count > 0 && !term(top = stack.Pop())) { yield return top; }
      if (!term(top)) otherwise();
    }
  }
}