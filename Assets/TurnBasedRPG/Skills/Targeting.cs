using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills.TargetFilters;
using UnityEngine;

namespace TurnBasedRPG.Skills {
    [Serializable]
    public class Targeting {
        public TargetGroup group = TargetGroup.Enemy;
        public TargetArea area = TargetArea.One;
        [SerializeReference, TypeInstance] public ITargetFilter filter = new Any();

        public bool SkipSelection(Character user, Data.Skill skill) => group == TargetGroup.Self;
        public bool ValidateTarget(Character user, Character target) => filter.Match(user, target);

        public IEnumerable<Character> GetAffectedTargets(Character user, Character target) => area switch
        {
            TargetArea.One => new[] { target }.Where(t => filter.Match(user, t)),
            TargetArea.Team => target.Allies.Where(t => filter.Match(user, t)),
            TargetArea.All => target.cm.everyone.Where(t => filter.Match(user, t)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public IEnumerable<Character> EligibleTargets(Character user) {
            var r = group switch
            {
                TargetGroup.Driven => throw new Exception(),
                TargetGroup.Self => new[] { user },
                TargetGroup.Enemy => user.Enemies,
                TargetGroup.Ally => user.Allies,
                TargetGroup.Enemies => user.Enemies,
                TargetGroup.Allies => user.Allies,
                TargetGroup.Everyone => user.cm.everyone,
                _ => throw new ArgumentOutOfRangeException()
            };
            return r.Where(t => filter.Match(user, t));
        }
    }
}