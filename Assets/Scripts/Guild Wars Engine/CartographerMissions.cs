using UnityEngine;

namespace Guild_Wars_Engine {
    public class CartographerMissions : MonoBehaviour {
        public void LoadBiome(int sceneId) => Game.Loading.ChangeScene(sceneId);
    }
}