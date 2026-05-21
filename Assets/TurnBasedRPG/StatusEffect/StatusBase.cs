using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffect
{
    [Preserve, Serializable]
    public abstract class StatusBase : Status
    {
        [Tooltip("-1 for infinity")]
        [SerializeField] private int maxDuration = 3;
        [SerializeField] private int maxStacks = 5;
        [SerializeField] private Observable<int> duration = new(3);
        [SerializeField] private Observable<int> stacks = new(1);

        protected virtual bool DisplayStacks => false;
        public Observable<int> Duration => duration;
        public Observable<int> Stacks { get; private set; } = new();
        public override Observable<int> DisplayValue => DisplayStacks ? stacks : duration;

        public override void Apply(Character target)
        {
            OnStacksChanged(target);
            target.OnStartTurn += StartTurn;
            target.OnEndTurn += EndTurn;
        }

        public override void Remove(Character target)
        {
            Stacks.Value = 0;
            OnStacksChanged(target);
        }

        protected virtual void StartTurn(Character target) { }

        protected virtual void EndTurn(Character target)
        {
            if (duration.Value < 0) return;
            duration.Value -= 1;
            if (duration.Value <= 0)
                target.StatusEffectList.Remove(source);
        }

        public override void Stack(Character target, Status other)
        {
            if (other is not StatusBase sb) return;
            duration.Value = Math.Min(duration.Value + sb.duration.Value, maxDuration);
            if (Stacks.Value >= maxStacks) return;
            Stacks.Value++;
            OnStacksChanged(target);
        }

        private void RemoveStack(Character target)
        {
            Stacks.Value--;
            if (Stacks.Value == 0)
                target.StatusEffectList.Remove(source);
            else OnStacksChanged(target);
        }

        protected virtual void OnStacksChanged(Character target) { }
    }
}