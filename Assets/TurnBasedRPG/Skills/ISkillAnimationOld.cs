using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TurnBasedRPG.Skills {
    public interface ISkillAnimationOld {
        bool TrySkipSelection(Character user, ISkill skill);
        bool ValidateTarget(Character user, Character target);
        IEnumerable<Character> GetAffectedTargets(Character user, Character target);
        IEnumerator Play(SkillArgs cast);

        static T Instantiate<T>(T a) where T : Component => Object.Instantiate(a);

        //public Targeting GetTargeting(ISkill skill);
        ISkillAnimation GetUpgrade(ISkill skill);
    }

    public interface ISkillAnimation : ISkillAnimationOld {
        //Targeting ISkillAnimationOld.GetTargeting(ISkill skill) => null;
        ISkillAnimation ISkillAnimationOld.GetUpgrade(ISkill skill) => this;

        bool ISkillAnimationOld.TrySkipSelection(Character user, ISkill skill) => throw new NotImplementedException();
        bool ISkillAnimationOld.ValidateTarget(Character user, Character target) => throw new NotImplementedException();
        IEnumerable<Character> ISkillAnimationOld.GetAffectedTargets(Character user, Character target) => throw new NotImplementedException();
    }
}