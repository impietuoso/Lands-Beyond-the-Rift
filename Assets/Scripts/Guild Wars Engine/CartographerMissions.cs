using UnityEngine;

public class CartographerMissions : MonoBehaviour {
    public void LoadBiome(int sceneId) {
        PersistenteConfig.Instance.load.ChangeScene(sceneId);
    }
}