using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;

public class StopSkillReactionEffect : ISkillEffect {
    public void Prepare(CombatArgs args) {
        args.stopReactionAttacks = true;
    }
}
