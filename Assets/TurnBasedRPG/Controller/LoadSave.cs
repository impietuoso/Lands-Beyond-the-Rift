using System;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;
using TurnBasedRPG.UI.Views;
using UnityEngine;

namespace TurnBasedRPG.Controller {
    [Obsolete]
    public class LoadSave : MonoBehaviour {
        public CombatManager combatManager;
        public CombatArena arena;
        public ListInventory<IItem> initialItems;
        public List<IPartyMember> encounters;
        public SaveView views;
        public ListView encounterListView;
        public SaveFile save;

        public void Awake() {
            encounterListView.SetData(encounters);

            if(PlayerPrefs.HasKey("SaveFile")) {
                var key = PlayerPrefs.GetString("SaveFile");
                save = JsonUtility.FromJson<SaveFile>(key);
            }
            else {
                save = new SaveFile
                {
                    currentParty = new ObservableList<IPartyMember>(),
                    players = new ObservableList<IPartyMember>(),
                    inventory = initialItems
                };

                while (save.currentParty.Count < 4)
                    save.currentParty.Add(null);

                while (save.players.Count < 8)
                    save.players.Add(null);

                views.SetData(save);
            }
        }
    }
}