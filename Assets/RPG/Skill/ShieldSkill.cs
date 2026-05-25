using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;

[Serializable, Obsolete]
public class ShieldSkill : ISkillEffectOld {
    public int shieldValue;
    public bool usePercentageOfHealth;

    public void Prepare(CombatArgs args) {
        if(usePercentageOfHealth)
            args.shield += (int)(args.target.Health.Max * shieldValue / 100f);
        else args.shield += shieldValue;
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) {
        if(usePercentageOfHealth) {
            return new PercentShield
            {
                percent = shieldValue / 100f,
            };
        }

        return new Shield
        {
            value = shieldValue,
            scale = new AttributeScale
            {
                scale = 0,
            }
        };
    }
}