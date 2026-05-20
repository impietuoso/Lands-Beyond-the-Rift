using TurnBasedRPG.Inventory;

namespace TurnBasedRPG.Data
{
    public class SaveFile {
        public ObservableList<PartyMember> currentParty;
        public ObservableList<PartyMember> players;
        public ListInventory<Item> inventory;
    }
}