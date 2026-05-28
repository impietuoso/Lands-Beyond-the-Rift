using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.Data {
    public interface ISkill {
        string SkillName { get; }
        string Description { get; }
        int Cost { get; }
        Sprite Icon { get; }
        Element Element { get; }
        Targeting Targeting { get; }
        IReadOnlyList<Tag> Tags { get; }
        ISkillAnimationOld Animation { get; }
        List<ISkillEffectOld> Effects { get; }
        bool Available(Character user);
        IEnumerator UseSkill(Character user, Character target);
    }
}