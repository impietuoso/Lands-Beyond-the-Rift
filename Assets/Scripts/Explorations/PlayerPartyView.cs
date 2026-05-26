using UnityEngine;

public class PlayerPartyView : DataView<PlayerParty> {
    [field: SerializeField] public ListView Members { get; private set; }
    [field: SerializeField] public IInventoryView Inventory { get; private set; }

    protected override void Subscribe() {
        Members.SetData(Data.members);
        Inventory.SetData(Data.inventory);
    }

    protected override void Unsubscribe() { }
}