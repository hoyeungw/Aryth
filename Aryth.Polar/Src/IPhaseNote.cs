using System.Collections.Generic;

namespace Aryth {
  public interface IPhaseNote<in TValue, TPhase, TCount> {
    IDictionary<TPhase, TCount> Counter { get; }
    TPhase Phase(TValue value);
    (TPhase phase, TCount count) Note(TValue value);
  }
}