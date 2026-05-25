using RPG;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorations {
    public class ItemDropParticleTest : MonoBehaviour
    {
        public Transform target;
        public ItemDropParticle itemDrop;
        public Item item;
        public int amount = 2;

        private void Update()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
                itemDrop.PopCopy(target.position, item, 1);
        }

        [ContextMenu("test pop")]
        public void TestPop() => itemDrop.PopCopy(target.position, item, amount);
    }
}