using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.PassiveTriggers {
    public class OnStartTurn : CharacterTrigger {
        protected override void SubscribeTrigger(Character c, Action<Character> a) => c.OnStartTurn += a;
        protected override void UnsubscribeTrigger(Character c, Action<Character> a) => c.OnStartTurn -= a;
    }

    public class OnEndTurn : CharacterTrigger {
        protected override void SubscribeTrigger(Character c, Action<Character> a) => c.OnEndTurn += a;
        protected override void UnsubscribeTrigger(Character c, Action<Character> a) => c.OnEndTurn -= a;
    }

    [Preserve, Serializable]
    public abstract class CharacterTrigger : IPassive {
        [SerializeReference, TypeInstance] private ITargetFilter condition = new TargetFilters.Any();
        [SerializeReference, TypeInstance] private ISkillEffectOld[] effects;

        protected abstract void SubscribeTrigger(Character c, Action<Character> a);
        protected abstract void UnsubscribeTrigger(Character c, Action<Character> a);
        public void Subscribe(Character character) => SubscribeTrigger(character, Execute);
        public void Unsubscribe(Character character) => UnsubscribeTrigger(character, Execute);

        private void Execute(Character e) {
            if(!condition.Match(e, e)) return;
            var args = new CombatArgs
            {
                source = this,
                user = e,
                target = e,
            };
            foreach (var effect in effects)
                effect.Prepare(args);
            args.Resolve();
        }
    }
}