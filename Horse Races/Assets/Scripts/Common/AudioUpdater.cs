using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(AudioSource))]
public class AudioUpdater : MonoBehaviour {
    enum AudioType { BGM, SFX, MASTER }
    [SerializeField] AudioType audioType;
    [SerializeField] AudioMixer mixer;

    private void Start() {
        AudioUpdate();
    }

    private void Update() {
        if (GameManager.Instance != null) {
            if (GameManager.Instance.MainMenu) {
                AudioUpdate();
            }
        }
    }

    public void AudioUpdate() {
        switch (audioType) {
            case AudioType.BGM:
                gameObject.GetComponent<AudioSource>().volume = GlobalVar.Instance.BGMVolume;
                break;
            case AudioType.SFX:
                //gameObject.GetComponent<AudioSource>().volume = GlobalVar.Instance.SFXVolume;
                AudioSource[] temp = gameObject.GetComponents<AudioSource>();
                foreach (AudioSource s in temp) {
                    s.volume = GlobalVar.Instance.SFXVolume;
                }
                break;
            case AudioType.MASTER:
                mixer.SetFloat("MasterVolume", Mathf.Log10(GlobalVar.Instance.MasterVolume)*20);
                break;
        }
    }
}
