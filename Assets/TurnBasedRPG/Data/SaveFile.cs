using System;

namespace TurnBasedRPG.Data
{
    [Serializable]
    public class SaveFile {
        public ObservableList<IPartyMember> currentParty;
        public ObservableList<IPartyMember> players;
        public ListInventory<IItem> inventory;
    }
}