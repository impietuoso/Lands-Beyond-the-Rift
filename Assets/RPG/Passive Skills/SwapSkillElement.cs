using TurnBasedRPG;
using TurnBasedRPG.Data;

public class SwapSkillElement : IPassiveSkill {
    public Element originalElement;
    public Element newElement;

    public void Subscribe(Character character) {
        character.OnAttack += SwapElement;
    }

    public void Unsubscribe(Character character) {
        character.OnAttack -= SwapElement;
    }

    public void SwapElement(CombatArgs args) {
        if(args.element && args.element == originalElement) {
            args.element = newElement;
        }
    }
}