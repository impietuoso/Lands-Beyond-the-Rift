using TurnBasedRPG.Data;

namespace TurnBasedRPG.Skills {
    public interface ISkillEffectOld {
        void Prepare(CombatArgs args);
        ISkillEffectOld GetUpgrade(Skill skill) => null;
    }

    public interface ISkillEffect : ISkillEffectOld {
        ISkillEffectOld ISkillEffectOld.GetUpgrade(Skill skill) => this;
    }

    public interface ICombatEffect {
        void Execute(CombatArgs args);
    }

    public interface IOnHitEffect {
        void OnHit(CombatArgs args);

        public void Prepare(CombatArgs args) {
            args.OnResolve += Effect;
        }

        private void Effect(CombatArgs args) {
            if(args.result.Miss) return;
            OnHit(args);
        }
    }
}