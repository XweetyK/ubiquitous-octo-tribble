using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] Slider SFXslider;
    [SerializeField] Slider BGMslider;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Image BGMselect;
    [SerializeField] Image SFXselect;
    [SerializeField] Image MasterSelect;
    int menu = 1;
    float bgm;
    int bgmCount;
    float sfx;
    int sfxCount;
    float master;
    int masterCount;

    private void Start() {
        LoadAudio();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (menu != 3) {
                menu++;
            }
            AudioManager.Instance.PlaySFX("Key");
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (menu != 1) {
                menu--;
            }
            AudioManager.Instance.PlaySFX("Key");
            UpdateUI();
        }

        if (menu == 1) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (masterCount < 10) {
                    masterCount++;
                    master = masterCount * 0.1f;
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (masterCount > 0) {
                    masterCount--;
                    if (masterCount == 0) {
                        master = 0.0001f;
                    } else {
                        master = masterCount * 0.1f;
                    }
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
        }
        if (menu == 2) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (bgmCount < 10) {
                    bgmCount++;
                    bgm = bgmCount * 0.1f;
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (bgmCount > 0) {
                    bgmCount--;
                    bgm = bgmCount * 0.1f;
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
        }
        if (menu == 3) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (sfxCount < 10) {
                    sfxCount++;
                    sfx = sfxCount * 0.1f;
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (sfxCount > 0) {
                    sfxCount--;
                    sfx = sfxCount * 0.1f;
                    AudioManager.Instance.PlaySFX("Key");
                }
                UpdateUI();
            }
        }
    }

    void UpdateUI() {
        BGMslider.value = bgm;
        SFXslider.value = sfx;
        MasterSlider.value = master;
        PlayerPrefs.SetFloat("BGM", bgm);
        PlayerPrefs.SetFloat("SFX", sfx);
        PlayerPrefs.SetFloat("MASTER", master);
        GlobalVar.Instance.BGMVolume = bgm;
        GlobalVar.Instance.SFXVolume = sfx;
        GlobalVar.Instance.MasterVolume = master;

        if (menu == 1) {
            MasterSelect.color = Color.red;
            BGMselect.color = Color.yellow;
            SFXselect.color = Color.yellow;
        }
        if (menu == 2) {
            MasterSelect.color = Color.yellow;
            BGMselect.color = Color.red;
            SFXselect.color = Color.yellow;
        }
        if (menu == 3) {
            MasterSelect.color = Color.yellow;
            BGMselect.color = Color.yellow;
            SFXselect.color = Color.red;
        }
    }

    public void LoadAudio() {
        bgm = PlayerPrefs.GetFloat("BGM");
        sfx = PlayerPrefs.GetFloat("SFX");
        master = PlayerPrefs.GetFloat("MASTER");
        bgmCount = (int)(bgm * 10);
        sfxCount = (int)(sfx * 10);
        masterCount = (int)(master * 10);
        UpdateUI();
    }
}
