namespace TurnBasedRPG.Skills {
    public interface IDamageModifier : ISkillEffect {
        public void ModifyArgs(CombatArgs args);
        void ISkillEffectOld.Prepare(CombatArgs args) => ModifyArgs(args);
    }
}