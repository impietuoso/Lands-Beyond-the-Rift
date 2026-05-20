using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRoom : MonoBehaviour
{
    [field: SerializeField] public bool StartingRoom { get; private set; }
    [field: SerializeField] public MinimapBuilder MinimapBuilder { get; private set; }
    [field: SerializeField] public GameObject Entities { get; private set; }
    [field: SerializeField] public TilemapRenderer Fog1 { get; private set; }
    [field: SerializeField] public TilemapRenderer Fog2 { get; private set; }
    [field: SerializeField] public TilemapCollider2D Collider { get; private set; }

    private void Start()
    {
        Entities.SetActive(StartingRoom);
        Collider.enabled = StartingRoom;
        Fog1.gameObject.SetActive(!StartingRoom);
        Fog2.gameObject.SetActive(!StartingRoom);
    }
}