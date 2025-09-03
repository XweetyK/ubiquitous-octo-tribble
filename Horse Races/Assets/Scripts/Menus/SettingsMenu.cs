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
    [SerializeField] GameObject ToggleCheck;
    [SerializeField] Text LanguageText;
    [SerializeField] Text ToggleText;
    int menu = 1;
    float bgm;
    int bgmCount;
    float sfx;
    int sfxCount;
    float master;
    int masterCount;
    Lang language = Lang.EN;

    private void Start() {
        LoadAudio();
        language = (Lang)PlayerPrefs.GetInt("Language");
        switch (language) {
            case Lang.EN:
                LanguageText.text = "  ENGLISH >";
                break;
            case Lang.ES:
                LanguageText.text = "< ESPAÑOL";
                break;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (menu != 5) {
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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            menu = 1;
        }

        if (menu == 1) {
            if (Input.GetButtonDown("Submit")) {
                if (ToggleCheck.activeSelf) {
                    ToggleCheck.SetActive(false);
                    Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                    //activate fullscreen
                }
                else {
                    ToggleCheck.SetActive(true);
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    //activate window mode
                }
            }
        }

        if (menu == 2) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                LanguageText.text = "  ENGLISH >";
                language = Lang.EN;
                PlayerPrefs.SetInt("Language", ((int)language));
                AudioManager.Instance.PlaySFX("Key");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                LanguageText.text = "< ESPAÑOL";
                language = Lang.ES;
                PlayerPrefs.SetInt("Language", ((int)language));
                AudioManager.Instance.PlaySFX("Key");
            }
        }

        if (menu == 3) {
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
        if (menu == 4) {
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
        if (menu == 5) {
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
            ToggleText.color = Color.red;
            LanguageText.color = Color.black;
            MasterSelect.color = Color.yellow;
            BGMselect.color = Color.yellow;
            SFXselect.color = Color.yellow;
        }
        if (menu == 2) {
            ToggleText.color = Color.black;
            LanguageText.color = Color.red;
            MasterSelect.color = Color.yellow;
            BGMselect.color = Color.yellow;
            SFXselect.color = Color.yellow;
        }
        if (menu == 3) {
            ToggleText.color = Color.black;
            LanguageText.color = Color.black;
            MasterSelect.color = Color.red;
            BGMselect.color = Color.yellow;
            SFXselect.color = Color.yellow;
        }
        if (menu == 4) {
            ToggleText.color = Color.black;
            LanguageText.color = Color.black;
            MasterSelect.color = Color.yellow;
            BGMselect.color = Color.red;
            SFXselect.color = Color.yellow;
        }
        if (menu == 5) {
            ToggleText.color = Color.black;
            LanguageText.color = Color.black;
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
