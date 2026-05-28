using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using UnityEngine.Scripting;

namespace RPG {
    [Preserve, Serializable]
    public class NoAnimation : ISkillAnimation {
        public IEnumerator Play(SkillArgs cast) {
            var args = new CombatArgs
            {
                source = this,
                skill = cast.Skill,
                element = cast.Skill.Element,
                user = cast.User,
                target = cast.Target,
            };

            foreach (var effect in cast.Skill.Effects)
                effect.Prepare(args);
            args.Resolve();
            yield break;
        }
    }
}