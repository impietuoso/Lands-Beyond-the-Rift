using TurnBasedRPG.Data;

namespace TurnBasedRPG.Controller {
    public class EquipmentController {
        private readonly LoadSave _load;
        private readonly EquipmentType[] _arrayOrder;

        public EquipmentController(LoadSave load, EquipmentType[] arrayOrder) {
            _load = load;
            _arrayOrder = arrayOrder;
        }

        public void EquipItem(IPartyMember member, IEquipment equip, int slot) {
            if(equip != null && equip.Type != _arrayOrder[slot]) return;

            if(member.Equips[slot] != null) {
                var old = member.Equips[slot];
                _load.save.inventory.Add(old, 1);
            }

            if(equip != null)
                _load.save.inventory.Remove(equip, 1);

            member.SetEquip(slot, equip);
        }
    }
}