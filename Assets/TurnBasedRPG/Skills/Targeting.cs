using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills.TargetFilters;
using UnityEngine;

namespace TurnBasedRPG.Skills {
    [Serializable]
    public class Targeting {
        public TargetGroup group = TargetGroup.Enemy;
        public TargetArea area = TargetArea.One;
        [SerializeReference, TypeInstance] public ITargetFilter filter = new Alive();

        public bool SkipSelection => group == TargetGroup.Self;
        public bool ValidateTarget(Character user, Character target) => filter.Match(user, target);

        public IEnumerable<Character> GetAffectedTargets(Character user, Character target) => area switch
        {
            TargetArea.One => new[] { target }.Where(t => filter.Match(user, t)),
            TargetArea.Team => target.Allies.Where(t => filter.Match(user, t)),
            TargetArea.All => target.cm.Everyone.Where(t => filter.Match(user, t)),
            _ => throw new ArgumentOutOfRangeException()
        };

        public IEnumerable<Character> EligibleTargets(Character user) {
            var r = group switch
            {
                TargetGroup.Self => new[] { user },
                TargetGroup.Ally => user.Allies,
                TargetGroup.Enemy => user.Enemies,
                TargetGroup.Any => user.cm.Everyone,
                _ => throw new ArgumentOutOfRangeException()
            };
            return r.Where(t => filter.Match(user, t));
        }
    }
}