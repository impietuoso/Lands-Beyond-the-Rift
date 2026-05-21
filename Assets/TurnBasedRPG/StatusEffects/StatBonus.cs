using System;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffect;

namespace TurnBasedRPG.StatusEffects
{
    [Serializable, Obsolete]
    public class StatBonus : StatusBase
    {
        public Stat stat;
        public float multiplier = 1.3f;

        public override void Apply(Character target)
        {
            base.Apply(target);
            target.Stats.GetStat(stat).AddBonus(this, multiplier);
        }

        public override void Remove(Character target)
        {
            base.Remove(target);
            target.Stats.GetStat(stat).RemoveBonus(this);
        }
    }
}