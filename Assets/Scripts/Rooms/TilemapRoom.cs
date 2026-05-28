using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rooms {
    public class TilemapRoom : MonoBehaviour {
        [field: SerializeField] public MinimapBuilder MinimapBuilder { get; private set; }
        [field: SerializeField] public GameObject Entities { get; private set; }
        [field: SerializeField] public TilemapCollider2D Collider { get; private set; }
        public TilemapRenderer Fog1 { get; private set; }
        public TilemapRenderer Fog2 { get; private set; }

        private void Awake() {
            Entities.SetActive(false);
            Collider.enabled = false;
        }

        public void SetFog(TilemapRenderer fog1, Material fogMat2) {
            Fog1 = fog1;
            Fog2 = Instantiate(Fog1, transform, true);
            Fog2.name = "Fog 2";
            Fog2.material = fogMat2;
            Fog2.sortingOrder++;
        }
    }
}