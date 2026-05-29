using UnityEngine;
using UnityEngine.Serialization;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/GameConfig", fileName = "New Game Config")]
    public class GameSettings : ScriptableObject {
        [field: SerializeField] public AudioClip ClickSfx { get; private set; }
        [field: SerializeField] public AudioClip ClickBlockSfx { get; private set; }
        
        public static GameSettings Instance => instance ??= Resources.Load<GameSettings>("GameConfig");
        private static GameSettings instance;
        [FormerlySerializedAs("equipmentOrder")]
        public EquipmentType[] equipmentArrayOrder;
        public int[] equipmentDrawOrder;

    }
}
