using System;
using UnityEngine;

namespace TurnBasedRPG.BattleStats {
    [Serializable]
    public class ResourceStat {
        public delegate void OnChangedHandle(ResourceStat stat, int delta);

        [SerializeField] private int max;
        [SerializeField] private int current;
        public float Normalized => Current / (float)Max;
        public event OnChangedHandle OnChanged;
        public event OnChangedHandle OnMaxChanged;

        public int Max {
            get => max;
            set {
                var delta = value - max;
                if (delta == 0) return;
                max = value;
                if (value < Current) Current = value;
                OnMaxChanged?.Invoke(this, delta);
            }
        }

        public int Current {
            get => current;
            set {
                value = Math.Clamp(value, 0, max);
                var delta = value - current;
                if (delta == 0) return;
                current = value;
                OnChanged?.Invoke(this, delta);
            }
        }

        public Result Add(int value) {
            value = Math.Clamp(current + value, 0, max);
            var delta = value - current;
            if (delta == 0) return new (current);
        
            current = value;
            OnChanged?.Invoke(this, delta);
            return new Result(current, delta);
        }

        public class Result {
            public Result(int final, int delta = 0) {
                Final = final;
                Delta = delta;
            }

            public int Final;
            public int Delta;
            public bool Fatal => Delta < 0 && Final == 0;
            public bool Revive => Delta > 0 && Final > 0;
        }
    }
}