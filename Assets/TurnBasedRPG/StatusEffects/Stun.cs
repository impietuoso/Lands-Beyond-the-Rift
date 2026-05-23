using System;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Stun : StatusBase {
        public override void Apply(Character target) {
            base.Apply(target);
            target.Stun.Add(Source);
        }

        public override void Remove(Character target) {
            base.Remove(target);
            target.Stun.Remove(Source);
        }
    }
}