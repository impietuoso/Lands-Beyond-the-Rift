using System.Linq;
using RPG;
using UnityEngine;

public class PlayerPartyView : DataView<PlayerParty> {
    [field: SerializeField] public PartyMemberView FirstMember { get; private set; }
    [field: SerializeField] public ListView Members { get; private set; }
    [field: SerializeField] public IInventoryView Inventory { get; private set; }

    protected override void Subscribe() {
        if(FirstMember) FirstMember.SetData(Data.members.FirstOrDefault());
        if(Members) Members.SetData(Data.members);
        if(Inventory) Inventory.SetData(Data.inventory);
    }

    protected override void Unsubscribe() { }
}