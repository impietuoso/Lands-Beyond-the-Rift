using System;
using TurnBasedRPG;
using TurnBasedRPG.Skill;

[Serializable]
public class ShieldSkill : ISkillEffect
{
    public int shieldValue;
    public bool usePercentageOfHealth;

    public void Prepare(CombatArgs args)
    {
        if (usePercentageOfHealth)
            args.shield += (int)(args.target.Health.Max * shieldValue / 100f);
        else args.shield += shieldValue;
    }
}