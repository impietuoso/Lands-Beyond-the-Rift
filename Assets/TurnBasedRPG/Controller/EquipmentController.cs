using TurnBasedRPG.Inventory;

namespace TurnBasedRPG.Data
{
    public static class EquipmentController {
        public static void EquipItem(LoadSave load, PartyMember member, Equipment newEquipment, int slot) {
            if(newEquipment && newEquipment.Type != GameConfig.Instance.equipmentArrayOrder[slot]) return;
        
            if (member.equips[slot]) {
                var oldEquipment = member.equips[slot];
                load.save.inventory.Add(oldEquipment, 1);
            }
        
            if (newEquipment)
                load.save.inventory.Remove(newEquipment, 1); 
        
            member.equips[slot] = newEquipment;
        }
    }
}
