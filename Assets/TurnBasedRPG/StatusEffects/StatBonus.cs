using System;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class StatBonus : StatusBase {
        [SerializeField, Label(null)] private Bonus bonus;

        protected override void OnStacksChanged(Character target, int amt) {
            base.OnStacksChanged(target, amt);
            bonus.Set(target.Stats, amt);
        }

        [Serializable]
        public class Bonus : ISingleLineDrawer {
            public enum Mode { Add, Mult, }

            public Stat stat;
            [Label(null)] public Mode mode = Mode.Mult;
            [Label(null)] public float value = 1.3f;

            public void Set(Stats stats, int stacks) {
                var s = stats.GetStat(stat);

                if (stacks == 0) {
                    if (mode == Mode.Mult)
                        s.RemoveBonusMult(this);
                    else
                        s.RemoveBonusAdd(this);
                } else {
                    if (mode == Mode.Mult)
                        s.SetBonusMult(this, (value - 1) * stacks + 1);
                    else
                        s.SetBonusAdd(this, value * stacks);
                }
            }
        }
    }
}
