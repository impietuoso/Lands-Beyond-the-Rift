using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

public class ApplySkillEffect : IPassiveSkill {
    public Element element;
    [SerializeReference, TypeInstance]
    public ISkillEffectOld effect;
    public void Subscribe(Character character) {
        character.OnAttack += ApplyEffect;
    }

    public void Unsubscribe(Character character) {
        character.OnAttack -= ApplyEffect;
    }

    public void ApplyEffect(CombatArgs args) {
        if (element && args.element != element) return;
        effect.Prepare(args);
    }
}