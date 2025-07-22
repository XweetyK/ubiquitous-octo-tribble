using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour {
    enum Next { MainMenu, StageSelect };
    SceneChanger sceneChanger;

    [SerializeField] Next next;
    public bool promptActive;

    [SerializeField] GameObject menu;
    [SerializeField] Text yes;
    [SerializeField] Text no;

    public bool idealRace;
    public bool stageSelect;

    int index = 0;

    Color initColor;

    private void Start() {
        sceneChanger = GameObject.FindObjectOfType<SceneChanger>();
        initColor = no.color;
        UpdateUi();
    }
    private void Update() {
        if (!idealRace) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (!stageSelect) {
                    if (!promptActive) {
                        menu.SetActive(true);
                        AudioManager.Instance.PauseAudio(0.1f);
                        AudioManager.Instance.PlaySFX("Select");
                    } else {
                        menu.SetActive(false);
                        AudioManager.Instance.NormalAudio(0.1f);
                    }
                    if (GameManager.Instance != null) {
                        GameManager.Instance.PauseGame();
                        AudioManager.Instance.PlaySFX("Cancel");
                    }
                    promptActive = !promptActive;
                    CheckDialogue();
                } else { sceneChanger.SwitchScene("MainMenu"); }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                AudioManager.Instance.PlaySFX("Wrong");
            }
        }

        if (Input.GetButtonDown("Submit")) {
            if (promptActive) {
                if (index == 1) {
                    AudioManager.Instance.PlaySFX("Select");
                    AudioManager.Instance.NormalAudio(0.1f);
                    switch (next) {
                        case Next.MainMenu:
                            sceneChanger.SwitchScene("MainMenu");
                            if (SceneManager.GetActiveScene().name != "PracticeScene") {
                                PlayerPrefs.SetString("Story", SceneManager.GetActiveScene().name);
                            }
                            break;
                        case Next.StageSelect:
                            sceneChanger.SwitchScene("StageSelect");
                            break;
                    }
                }
                if (index == 0) {
                    if (GameManager.Instance != null) {
                        GameManager.Instance.PauseGame();
                    }
                    promptActive = !promptActive;
                    menu.SetActive(false);
                    AudioManager.Instance.PlaySFX("Cancel");
                    AudioManager.Instance.NormalAudio(0.1f);
                    CheckDialogue();
                }
            }
        }

        if (promptActive) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                index = 1;
                UpdateUi();
                AudioManager.Instance.PlaySFX("Key");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                index = 0;
                UpdateUi();
                AudioManager.Instance.PlaySFX("Key");
            }
        }
    }

    void UpdateUi() {
        if (index == 1) {
            yes.color = Color.yellow;
            no.color = initColor;
        } else {
            yes.color = initColor;
            no.color = Color.yellow;
        }
    }

    void CheckDialogue() {
        if (promptActive) {
            Fungus.DialogInput[] dials = GameObject.FindObjectsOfType<Fungus.DialogInput>();
            if (dials != null) {
                foreach (var dial in dials) {
                    dial.onPause = !dial.onPause;
                }
            }
        } else { Invoke("ResumeDialogue", 0.5f); }
    }
    void ResumeDialogue() {
        Fungus.DialogInput[] dials = GameObject.FindObjectsOfType<Fungus.DialogInput>();
        foreach (var dial in dials) {
            dial.onPause = !dial.onPause;
        }
    }
}
