using System.Collections;
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

        public IEnumerator CreateFog(Tile tile, Material fog1, Material fog2) {
            var sourceMap = MinimapBuilder.tilemap;
            yield return CreateFogLayer(sourceMap, tile, fog1);

            Fog2 = Instantiate(Fog1, transform);
            Fog2.name = "Fog 2";
            Fog2.material = fog2;
        }

        private IEnumerator CreateFogLayer(Tilemap source, Tile tile, Material mat) {
            var go = new GameObject("Fog 1");
            go.transform.SetParent(transform, false);

            var map = go.AddComponent<Tilemap>();
            go.AddComponent<TilemapRenderer>().material = mat;

            var count = 0;
            foreach (var pos in source.cellBounds.allPositionsWithin) {
                if(source.HasTile(pos)) {
                    map.SetTile(pos, tile);
                    count++;
                }

                if(count % Game.TileBuildSpeed == 0) yield return null;
            }
        }
    }
}