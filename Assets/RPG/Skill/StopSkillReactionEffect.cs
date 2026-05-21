using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;

[Serializable, Obsolete]
public class StopSkillReactionEffect : ISkillEffectOld {
    public void Prepare(CombatArgs args) {
        args.stopReactionAttacks = true;
    }
}
