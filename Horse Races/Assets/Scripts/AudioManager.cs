using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] SFX;
    public Sound[] BGM;
    public Sound[] AMB;

    [SerializeField] GameObject SFXSource;
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource AMBSource;
    [SerializeField] AudioMixerSnapshot[] glitch;
    [SerializeField] AudioMixerSnapshot pause;
    [SerializeField] AudioMixerSnapshot normal;
    [SerializeField] AudioMixerGroup SFXMixer;

    bool fadingOut;
    bool fadingIn;
    public bool glitching;

    float fadeOutTime;
    float fadeInTime;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) {
            Instance = this;
        } else { Destroy(gameObject); }
    }
    private void Start() {
        foreach (Sound s in SFX) {
            AudioSource temp = SFXSource.AddComponent<AudioSource>();
            temp.playOnAwake = false;
            temp.loop = false;
            temp.outputAudioMixerGroup = SFXMixer;

            s.source = temp;
            s.source.clip = s.clip;
            Invoke("Timer", 1f);
        }
    }

    public void PlaySFX(string name) {
        System.Array.Find(SFX, sound => sound.name == name).Play();
    }
    public void PlayJump() {
        Sound temp = System.Array.Find(SFX, sound => sound.name == "Jump");
        temp.source.pitch = Random.Range(0.7f, 1.3f);
        temp.Play();
    }

    public void PlayBGM(string name, float inTime, float outTime) {
        BGMSource.clip= System.Array.Find(BGM, sound => sound.name == name).clip;
        FadeIn(inTime);
        BGMSource.Play();
    }

    void FadeIn(float time) {
        fadingOut = false;
        BGMSource.volume = 0f;
        fadingIn = true;
        fadeInTime = time;
    }

    public void FadeOut(float time) {
        fadingIn = false;
        fadingOut = true;
        fadeOutTime = time;
    }

    public void GlitchAudio() {
        glitch[0].TransitionTo(0.1f);
    }
    public void NormalAudio(float time) {
        normal.TransitionTo(time);
        glitching = false;
    }
    public void PauseAudio(float time) {
        pause.TransitionTo(time);
    }

    public void GlitchExtraAudio() {
        glitching = true;
    }

    private void Update() {
        //FADE OUT---------------------------------------
        if (fadingOut) {
            if (BGMSource.volume != 0f) {
                //BGMSource.volume -= 0.01f * fadeInTime;
                BGMSource.volume = Mathf.Lerp(BGMSource.volume, 0, fadeOutTime * Time.deltaTime);
                if (BGMSource.volume < 0.005f) {
                    BGMSource.volume = 0f;
                }
            }
        }
        //FADE IN---------------------------------------
        if (fadingIn) {
            if (BGMSource.volume != GlobalVar.Instance.BGMVolume) {
                //BGMSource.volume += 0.01f * fadeInTime;
                BGMSource.volume = Mathf.Lerp(BGMSource.volume, GlobalVar.Instance.BGMVolume, fadeInTime * Time.deltaTime);
                //if (BGMSource.volume > (GlobalVar.Instance.BGMVolume-0.005f)) {
                //    BGMSource.volume = GlobalVar.Instance.BGMVolume;
                //}
            }
        }
    }

    void Timer() {
        if (glitching) {
            glitch[Random.Range(0, glitch.Length - 1)].TransitionTo(0.5f);
        }
        Invoke("Timer", Random.Range(2f,5f));
    }
}
