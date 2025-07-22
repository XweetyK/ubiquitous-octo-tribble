using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public enum AudioType { BGM, SFX};
    public AudioType audioType;
    public string trackName;
    public float fadeInTime;
    public float fadeOutTime;
    public float slowPauseTime;
    public bool playOnStart;
    public bool glitchOnStart;
    public bool extraGlitchOnStart;

    private void Start() {
        if (playOnStart) {
            switch (audioType) {
                case AudioType.BGM:
                    PlayBGM();
                    break;
                case AudioType.SFX:
                    PlaySFX();
                    break;
            }
        }
        if (glitchOnStart) {
            Glitch();
        }
        if (extraGlitchOnStart) {
            ExtraGlitch();
        }
    }

    public void PlaySFX() {
        AudioManager.Instance.PlaySFX(trackName);
    }
    public void PlayBGM() {
        AudioManager.Instance.PlayBGM(trackName, fadeInTime, fadeOutTime);
    }
    
    public void Glitch() {
        AudioManager.Instance.GlitchAudio();
    }
    public void ExtraGlitch() {
        AudioManager.Instance.GlitchExtraAudio();
    }
    public void Normal() {
        AudioManager.Instance.NormalAudio(1f);
    }
    public void SlowPause() {
        AudioManager.Instance.PauseAudio(slowPauseTime);
    }
    public void StopBGM() {
        AudioManager.Instance.FadeOut(fadeOutTime);
    }
}
