using System;
using TurnBasedRPG;
using TurnBasedRPG.Skill;

public class StopSkillReactionEffect : ISkillEffect {
    public void Prepare(CombatArgs args) {
        args.stopReactionAttacks = true;
    }
}
