using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.PassiveTriggers {
    public class OnAttack : CombatArgsTrigger {
        protected override void SubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnAttack += a;
        protected override void UnsubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnAttack -= a;
    }

    public class OnDefend : CombatArgsTrigger {
        protected override void SubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnDefend += a;
        protected override void UnsubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnDefend -= a;
    }

    public class OnResolveAttack : CombatArgsTrigger {
        protected override void SubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnResolveAttack += a;
        protected override void UnsubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnResolveAttack -= a;
    }

    public class OnResolveDefend : CombatArgsTrigger {
        protected override void SubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnResolveDefend += a;
        protected override void UnsubscribeTrigger(Character c, Action<CombatArgs> a) => c.OnResolveDefend -= a;
    }

    [Preserve, Serializable]
    public abstract class CombatArgsTrigger : IPassive {
        [SerializeReference, TypeInstance] private ICombatArgsFilter condition = new CombatArgsFilters.None();
        [SerializeReference, TypeInstance] private ISkillEffectOld[] effects;

        protected abstract void SubscribeTrigger(Character c, Action<CombatArgs> a);
        protected abstract void UnsubscribeTrigger(Character c, Action<CombatArgs> a);
        public void Subscribe(Character character) => SubscribeTrigger(character, Execute);
        public void Unsubscribe(Character character) => UnsubscribeTrigger(character, Execute);

        private void Execute(CombatArgs e) {
            if(!condition.Match(e)) return;
            foreach (var effect in effects)
                effect.Prepare(e);
        }
    }
}