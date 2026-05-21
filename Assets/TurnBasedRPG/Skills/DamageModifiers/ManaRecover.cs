using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.DamageModifiers {
    [Preserve, Serializable]
    public class ManaRecover : IDamageModifier {
        public void ModifyArgs(CombatArgs args) {
            args.OnResolve += Recover;
        }

        private void Recover(CombatArgs args) {
            if(args.result.Miss) return;
            var amt = args.user.Mana.Max * .25f;
            args.user.Mana.Current += (int)amt;
        }
    }
}