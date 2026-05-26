using System.Collections.Generic;
using RPG;
using TurnBasedRPG.Data;
using UnityEngine;

public class PlayerParty : MonoBehaviour {
    public ObservableList<IPartyMember> members;
    public ListInventory<Item> inventory;

    public IEnumerable<(IConsumable, int)> GetConsumables() {
        foreach (var slot in inventory.slots)
            if(slot.item is IConsumable consumable)
                yield return (consumable, slot.amount);
    }
}