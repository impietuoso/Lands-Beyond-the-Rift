using System.Collections.Generic;
using Data;
using TurnBasedRPG.Data;
using UnityEngine;

namespace Explorations {
    public class PlayerParty : MonoBehaviour {
        public ObservableList<PartyMember> members;
        public ListInventory<Item> inventory;

        public IEnumerable<(IConsumable, int)> GetConsumables() {
            foreach (var m in members)
                if(m)
                    foreach (var c in m.Consumables)
                        yield return (c, 1);
        }
    }
}