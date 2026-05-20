using TurnBasedRPG.Skill;

namespace TurnBasedRPG
{
    public class EffectAttribute : TypeDropdownAttribute {
        public EffectAttribute() : base(typeof(ISkillEffect)) {
        
        }
    }
}