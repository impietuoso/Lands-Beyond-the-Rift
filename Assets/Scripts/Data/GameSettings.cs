using TurnBasedRPG.Data;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/GameConfig", fileName = "New Game Config")]
    public class GameSettings : ScriptableObject {
        [field: SerializeField] public AudioClip ClickSfx { get; private set; }
        [field: SerializeField] public AudioClip ClickBlockSfx { get; private set; }
        [field: SerializeField] public Skill BasicDefense { get; private set; }

        public EquipmentType[] equipmentArrayOrder;
        public int[] equipmentDrawOrder;
    }
}