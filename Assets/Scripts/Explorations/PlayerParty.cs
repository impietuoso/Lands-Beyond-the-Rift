using System.Collections.Generic;
using RPG;
using TurnBasedRPG.Data;
using UnityEngine;

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