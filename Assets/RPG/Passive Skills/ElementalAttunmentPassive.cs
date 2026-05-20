using TurnBasedRPG;
using TurnBasedRPG.Data;

public class ElementalAttunmentPassive : IPassiveSkill {
    public Element element;
    public void Subscribe(Character character) {
        character.Element = element;
    }

    public void Unsubscribe(Character character) {
        character.Element = character.member.element;
    }
}