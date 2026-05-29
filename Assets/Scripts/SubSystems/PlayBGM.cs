using UnityEngine;

namespace SubSystems {
    public class PlayBGM : MonoBehaviour {
        public AudioClip bgm;

        public void OnEnable() {
            Game.Audio.PlayBGM(bgm, true);
        }
    }
}