using System;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Silence : StatusBase {
        public override void Apply(Character target) {
            base.Apply(target);
            target.Silence.Add(Source);
        }

        public override void Remove(Character target) {
            base.Remove(target);
            target.Silence.Remove(Source);
        }
    }
}