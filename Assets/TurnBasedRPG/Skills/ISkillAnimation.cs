using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TurnBasedRPG.Skills {
    public interface ISkillAnimation {
        public bool TrySkipSelection(Character user, Skill skill);
        public bool ValidateTarget(Character user, Character target);
        public IEnumerable<Character> GetAffectedTargets(Character user, Character target);
        [Obsolete] public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm);
        public IEnumerator Play(SkillArgs cast) => Play(cast.Skill, cast.User, cast.Target, cast.Cm);

        public static T Instantiate<T>(T a) where T : Component => Object.Instantiate(a);
    }
}