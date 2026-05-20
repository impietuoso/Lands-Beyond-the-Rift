using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRoom : MonoBehaviour
{
    [field: SerializeField] public MinimapBuilder MinimapBuilder { get; private set; }
    [field: SerializeField] public GameObject Entities { get; private set; }
    [field: SerializeField] public TilemapRenderer Fog1 { get; private set; }
    [field: SerializeField] public TilemapRenderer Fog2 { get; private set; }
}