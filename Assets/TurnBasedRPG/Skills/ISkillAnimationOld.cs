using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TurnBasedRPG.Skills {
    public interface ISkillAnimationOld {
        public bool TrySkipSelection(Character user, Skill skill);
        public bool ValidateTarget(Character user, Character target);
        public IEnumerable<Character> GetAffectedTargets(Character user, Character target);
        [Obsolete] public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm);
        public IEnumerator Play(SkillArgs cast) => Play(cast.Skill, cast.User, cast.Target, cast.Cm);

        public static T Instantiate<T>(T a) where T : Component => Object.Instantiate(a);

        public Targeting GetTargeting(Skill skill);
        //public ISkillAnimationOld GegUpgrade(Skill skill);
    }

    public interface ISkillAnimation : ISkillAnimationOld {
        Targeting ISkillAnimationOld.GetTargeting(Skill skill) => null;
        public ISkillAnimationOld GetUpgrade(Skill skill) => this;

        bool ISkillAnimationOld.TrySkipSelection(Character user, Skill skill) => throw new NotImplementedException();
        bool ISkillAnimationOld.ValidateTarget(Character user, Character target) => throw new NotImplementedException();
        IEnumerable<Character> ISkillAnimationOld.GetAffectedTargets(Character user, Character target) => throw new NotImplementedException();
    }
}