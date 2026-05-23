using System;
using TurnBasedRPG.Data;

namespace TurnBasedRPG.StatusEffects {
    public abstract class Status {
        public StatusSO Source { get; set; }
        public abstract void Apply(Character target);
        public abstract void Remove(Character target);
        public abstract void Stack(Character target, Status other);
        public abstract IObservable<int> DisplayValue { get; }

        protected bool TryNullifyOpposite(Character _) => throw new InvalidOperationException();
    }
}