using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.DamageModifiers;

[Serializable, Obsolete]
public class StopSkillReactionEffect : ISkillEffectOld {
    public void Prepare(CombatArgs args) {
        args.stopReactionAttacks = true;
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) {
        return new StopReactions();
    }
}
