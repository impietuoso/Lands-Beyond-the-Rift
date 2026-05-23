using System;
using UnityEngine;

public class PlayBGM : MonoBehaviour {
    public AudioClip bgm;

    public void OnEnable() {
        Game.Audio.PlayBGM(bgm, true);
    }
}