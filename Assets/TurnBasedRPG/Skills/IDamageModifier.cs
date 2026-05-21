namespace TurnBasedRPG.Skills {
    public interface IDamageModifier : ISkillEffect {
        public void ModifyArgs(CombatArgs args);
        void ISkillEffect.Prepare(CombatArgs args) => ModifyArgs(args);
    }
}