using System;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class StatBonus : StatusBase {
        [SerializeField, Label(null)] private Bonus bonus;

        public override void Apply(Character target) {
            base.Apply(target);
            bonus.Add(target.Stats);
        }

        public override void Remove(Character target) {
            base.Remove(target);
            bonus.Remove(target.Stats);
        }

        [Serializable]
        public class Bonus : ISingleLineDrawer {
            public enum Mode {
                Add,
                Mult,
            }

            public Stat stat;
            [Label(null)] public Mode mode = Mode.Mult;
            [Label(null)] public float value = 1.3f;

            public void Add(Stats stats) {
                var s = stats.GetStat(stat);
                if(mode == Mode.Add)
                    s.Base += (int)value;
                else s.AddBonus(this, value);
            }

            public void Remove(Stats stats) {
                var s = stats.GetStat(stat);
                if(mode == Mode.Add)
                    s.Base -= (int)value;
                else s.RemoveBonus(this);
            }
        }
    }
}