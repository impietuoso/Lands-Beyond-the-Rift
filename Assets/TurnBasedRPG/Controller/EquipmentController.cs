using TurnBasedRPG.Data;

namespace TurnBasedRPG.Controller {
    public static class EquipmentController {
        public static void EquipItem(LoadSave load, IPartyMember member, IEquipment equip, int slot) {
            if(equip != null && equip.Type != GameSettings.Instance.equipmentArrayOrder[slot]) return;

            if(member.Equips[slot] != null) {
                var old = member.Equips[slot];
                load.save.inventory.Add(old, 1);
            }

            if(equip != null)
                load.save.inventory.Remove(equip, 1);

            member.SetEquip(slot, equip);
        }
    }
}