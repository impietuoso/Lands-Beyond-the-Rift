using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public abstract class StatusBase : Status {
        [Tooltip("-1 for infinity")]
        [SerializeField, SingleLine] private StatusValue duration = new(3);
        [SerializeField, SingleLine] private StatusValue stacks = new(1);

        public IObservable<int> Duration => duration;
        public IObservable<int> Stacks => stacks;
        public override IObservable<int> DisplayValue => duration;

        public override void Apply(Character target) {
            target.OnStartTurn += StartTurn;
            target.OnEndTurn += EndTurn;
            OnStacksChanged(target, stacks.Value);
        }

        public override void Remove(Character target) {
            stacks.Value = 0;
            OnStacksChanged(target, 0);
        }

        protected virtual void StartTurn(Character target) { }

        protected virtual void EndTurn(Character target) {
            if(duration.Value < 0) return;
            duration.Value -= 1;
            if(duration.Value <= 0)
                target.StatusEffectList.Remove(Source);
        }

        public override void Stack(Character target, Status other) {
            if(other is not StatusBase sb) return;
            duration.Value += sb.duration.Value;
            if(stacks.Value >= stacks.Max) return;
            stacks.Value++;
            OnStacksChanged(target, stacks.Value);
        }

        private void RemoveStack(Character target) {
            stacks.Value--;
            if(Stacks.Value == 0)
                target.StatusEffectList.Remove(Source);
            else OnStacksChanged(target, stacks.Value);
        }

        protected virtual void OnStacksChanged(Character target, int amt) { }
    }
    
    [Serializable]
    public class StatusValue : IObservable<int>, ISingleLineDrawer {
        [SerializeField] private int value, max;

        public int Value
        {
            get => value;
            set {
                var v = Math.Clamp(value, 0, max);
                if(this.value == v) return;
                this.value = v;
                OnChange?.Invoke(v);
            }
        }

        public int Max => max;
        public event Action<int> OnChange;
        public StatusValue(int v) => value = max = v;
    }
}