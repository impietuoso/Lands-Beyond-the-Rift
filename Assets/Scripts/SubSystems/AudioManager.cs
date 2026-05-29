using UnityEngine;
using UnityEngine.Audio;

namespace SubSystems {
    public class AudioManager : MonoBehaviour {
        [Header("Audio Mixers")]
        public AudioMixer mixer;
        public AudioSource bgmAudioSource;
        public AudioSource sfxAudioSource;

        private void Start() {
            LoadVolumes();
        }

        public void PlayBGM(AudioClip clip, bool loop = false) {
            if (bgmAudioSource == null || clip == null) return;
            if (bgmAudioSource.isPlaying && bgmAudioSource.clip == clip) return;

            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = loop;
            bgmAudioSource.Play();
        }

        public void StopAudio() {
            if (bgmAudioSource.isPlaying) {
                bgmAudioSource.Stop();
            }
        }

        public void PlaySFX(AudioClip clip) {
            if (sfxAudioSource == null || clip == null) return;

            sfxAudioSource.PlayOneShot(clip);
        }
    

        public void LoadVolumes() {
            var MasterVolume = PlayerPrefs.GetFloat("Master", 0.5f);
            var BGMVolume = PlayerPrefs.GetFloat("BGM", 0.5f);
            var SFXVolume = PlayerPrefs.GetFloat("SFX", 0.5f);
        
            mixer.SetFloat("Master", Mathf.Log10(MasterVolume) * 20);
            mixer.SetFloat("BGM", Mathf.Log10(BGMVolume) * 20);
            mixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20);
        }
    
    }
}