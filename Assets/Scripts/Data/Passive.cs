using TurnBasedRPG;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/Passive")]
    public class Passive : Item, IPassive {
        [Separator]
        [SerializeReference, TypeInstance] public IPassive trigger;
        public void Subscribe(Character character) => trigger.Subscribe(character);
        public void Unsubscribe(Character character) => trigger.Unsubscribe(character);
    }
}