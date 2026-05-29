using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG.BattleStats {
    [Serializable]
    public class BonusStat {
        [SerializeField] private int min, max = 999;
        [SerializeField] private int @base, total;

        private readonly Dictionary<object, float> _bonusAdd = new ();
        private readonly Dictionary<object, float> _bonusMult = new ();

        public int Total => total;
        public float Normalized => Total / (float)Max;
        public event Action<int> OnChanged;
        public static implicit operator int(BonusStat stat) => stat.Total;

        public int Min {
            get => min;
            set {
                min = value;
                Recalculate();
            }
        }

        public int Max {
            get => max;
            set {
                max = value;
                Recalculate();
            }
        }

        public int Base {
            get => @base;
            set {
                @base = value;
                Recalculate();
            }
        }

        public void SetBonusAdd(object key, float value) {
            _bonusAdd[key] = value;
            Recalculate();
        }

        public void RemoveBonusAdd(object key) {
            if (_bonusAdd.Remove(key))
                Recalculate();
        }
        
        public void SetBonusMult(object key, float value) {
            _bonusMult[key] = value;
            Recalculate();
        }

        public void RemoveBonusMult(object key) {
            if (_bonusMult.Remove(key))
                Recalculate();
        }

        public void Recalculate() {
            float v = Base;
            
            foreach (var bonus in _bonusAdd)
                v += bonus.Value;
            
            foreach (var bonus in _bonusMult)
                v *= bonus.Value;

            total = Mathf.Clamp((int)v, Min, Max);
            OnChanged?.Invoke(Total);
        }
    }
}