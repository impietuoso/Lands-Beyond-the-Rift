using System.Collections.Generic;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;
using TurnBasedRPG.UI.Views;
using UnityEngine;

namespace TurnBasedRPG.Controller {
    public class LoadSave : MonoBehaviour {
        public CombatManager combatManager;
        public CombatArena arena;
        public List<PartyMember> initialParty;
        public List<PartyMember> availableCharacters;
        public ListInventory<Item> initialItems;
        public List<EnemyEncounter> encounters;
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
                    currentParty = new ObservableList<PartyMember>(),
                    players = new ObservableList<PartyMember>(),
                    inventory = initialItems
                };

                foreach (var character in initialParty)
                    save.currentParty.Add(Instantiate(character));

                while (save.currentParty.Count < 4)
                    save.currentParty.Add(null);

                foreach (var character in availableCharacters)
                    save.players.Add(Instantiate(character));

                while (save.players.Count < 8)
                    save.players.Add(null);

                views.SetData(save);
            }
        }

        public void StartBattle(EnemyEncounterView encounter) {
            var config = new CombatConfig
            {
                Arena = arena,
                Allies = save.currentParty,
                Enemies = encounter.Data.enemyList,
                Items = save.inventory.slots,
            };
            combatManager.StartCombat(config);
        }
    }
}