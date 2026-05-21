using TurnBasedRPG.DrawerHelpers;

namespace TurnBasedRPG.Skills.SkillEffects {
    public class Mana : ICombatEffect, IDamageModifier, ISingleLineDrawer {
        public int amount;
        public bool tgtUser;

        public void Prepare(CombatArgs args) {
            args.OnResolve += Add;
        }

        private void Add(CombatArgs args) {
            var tgt = tgtUser ? args.user : args.target;
            tgt.Mana.Current += amount;
        }

        public void ModifyArgs(CombatArgs args) {
            args.OnResolve += AddOnHit;
        }

        private void AddOnHit(CombatArgs args) {
            if(args.result.Miss) return;
            var tgt = tgtUser ? args.user : args.target;
            tgt.Mana.Current += amount;
        }
    }
}