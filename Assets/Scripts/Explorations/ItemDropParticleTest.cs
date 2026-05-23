using UnityEngine;
using UnityEngine.InputSystem;

namespace Explorations {
    public class ItemDropParticleTest : MonoBehaviour
    {
        public Transform target;
        public ItemDropParticle itemDrop;
        public Sprite sprite;
        public int amount = 2;

        private void Update()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
                itemDrop.PopCopy(target.position, sprite, 1);
        }

        [ContextMenu("test pop")]
        public void TestPop() => itemDrop.PopCopy(target.position, sprite, amount);
    }
}