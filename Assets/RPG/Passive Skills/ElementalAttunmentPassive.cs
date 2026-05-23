using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;


public class ElementalAttunmentPassive : IPassive {
    public Element element;
    public void Subscribe(Character character) {
        character.Element = element;
    }

    public void Unsubscribe(Character character) {
        character.Element = character.member.element;
    }
}