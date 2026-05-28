using System.Collections;
using TurnBasedRPG.Data;

namespace TurnBasedRPG.Skills {
    public interface ISkillAnimationOld {
        IEnumerator Play(SkillArgs cast);
        ISkillAnimation GetUpgrade(ISkill skill);
    }

    public interface ISkillAnimation : ISkillAnimationOld {
        //Targeting ISkillAnimationOld.GetTargeting(ISkill skill) => null;
        ISkillAnimation ISkillAnimationOld.GetUpgrade(ISkill skill) => this;
    }
}