using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TurnBasedRPG.Controller {
    public class EquipmentManager : MonoBehaviour {
        public PartyMemberView memberView;
        public LoadSave load;
        public ListView inventoryView;
        public EquipmentView newItensStats;
        public EquipmentView currentItemStats;
        public Button swapButton;
        private int selectedItemIndex;
        private EquipmentType equipFilter;
        private int[] equipmentDrawOrder;
        private EquipmentType[] equipmentArrayOrder;
        private EquipmentController _controller;

        private void OnEnable() {
            ApplyFilter((EquipmentType)null);
            _controller ??= new EquipmentController(load, equipmentArrayOrder);
        }

        public void ResetFilter() {
            var itensToFilter = load.save.inventory.slots.Where(i => i.item is IEquipment);
            inventoryView.SetData(itensToFilter);
        }

        public void ApplyFilter(EquipmentView view) {
            int orderIndex = equipmentDrawOrder[view.transform.GetSiblingIndex() - 1];
            equipFilter = equipmentArrayOrder[orderIndex];
            ApplyFilter(equipFilter);
        }

        public void ApplyFilter(EquipmentType newFilter) {
            memberView.SetData(memberView.Data);
            equipFilter = newFilter;
            if(newFilter == null) {
                ResetFilter();
                return;
            }

            if(newItensStats.Data != null && newItensStats.Data.Type != newFilter) newItensStats.SetData(null);
            var itensToFilter = load.save.inventory.slots.Where(i => i.item is IEquipment e && e.Type == newFilter);
            inventoryView.SetData(itensToFilter);
            swapButton.interactable = false;
        }


        public void SelectNewEquipButton() {
            List<int> weaponIndexes = new();
            for (int i = 0; i < memberView.Data.Equips.Count; i++) {
                var type = equipmentArrayOrder[i];
                if(type == newItensStats.Data.Type) {
                    weaponIndexes.Add(i);
                }
            }

            var equipData = memberView.Data.Equips[weaponIndexes[0]];
            currentItemStats.SetData(equipData);
            selectedItemIndex = weaponIndexes[0];
            swapButton.interactable = true;
        }

        public void SwapEquipmentButton() {
            if(newItensStats.Data == null) return;

            var removedEquipment = memberView.Data.Equips[selectedItemIndex];

            _controller.EquipItem(memberView.Data, newItensStats.Data, selectedItemIndex);

            newItensStats.SetData(removedEquipment);
            currentItemStats.SetData(memberView.Data.Equips[selectedItemIndex]);
            ApplyFilter(currentItemStats.Data.Type);
        }


        public void SwapFromEquipment(GameObject drop, PointerEventData eventData) {
            if(eventData.pointerDrag.transform.parent == drop.transform.parent) return;

            var inventoryItem = drop.GetComponent<EquipmentView>().Data;
            var drawIndex = eventData.pointerDrag.transform.GetSiblingIndex() - 1;
            var equipedItemIndex = equipmentDrawOrder[drawIndex];

            _controller.EquipItem(memberView.Data, inventoryItem, equipedItemIndex);

            ApplyFilter(equipFilter);
        }

        public void SwapFromInventory(GameObject drop, PointerEventData eventData) {
            if(eventData.pointerDrag.transform.parent == drop.transform.parent) return;

            var inventoryItem = eventData.pointerDrag.GetComponent<EquipmentView>().Data;
            var drawIndex = drop.transform.GetSiblingIndex() - 1;
            var equipedItemIndex = equipmentDrawOrder[drawIndex];

            _controller.EquipItem(memberView.Data, inventoryItem, equipedItemIndex);

            ApplyFilter(equipFilter);
        }

        public void UnequipEquipment(EquipmentView view) {
            if(view.Data == null) return;

            var drawIndex = view.transform.GetSiblingIndex() - 1;
            var targetIndex = equipmentDrawOrder[drawIndex];

            _controller.EquipItem(memberView.Data, null, targetIndex);

            ApplyFilter(equipFilter);
        }
    }
}