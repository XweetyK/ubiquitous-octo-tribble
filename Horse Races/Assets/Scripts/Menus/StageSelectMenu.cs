using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kino;

public class StageSelectMenu : MonoBehaviour {
    [SerializeField] Text trackUI;
    [SerializeField] string[] trackName;
    [SerializeField] Image mapUI;
    [SerializeField] Sprite[] mapImg;
    [SerializeField] Sprite specialMap;
    [SerializeField] string specialText;
    [SerializeField] string[] sceneName;
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] Image winnerUI;
    [SerializeField] Sprite[] winnerImg;
    [SerializeField] Text winnerName;
    [SerializeField] Image sticker;
    int[] winners;
    public int limit;
    int track = 1;
    GameObject cam;
    bool special = false;

    private void Start() {
        cam = GameObject.FindObjectOfType<Camera>().gameObject;
        winners = new int[7];
        CheckWinner();
        UpdateUI();
        AudioManager.Instance.NormalAudio(0.1f);
        AudioManager.Instance.PlayBGM("StageSelect", 1f, 0.5f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (track < limit) {
                track++;
            } else { track = 1; }
            UpdateUI();
            AudioManager.Instance.PlaySFX("Key");
            AudioManager.Instance.NormalAudio(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (track > 1) {
                track--;
            } else { track = limit; }
            UpdateUI();
            AudioManager.Instance.PlaySFX("Key");
            AudioManager.Instance.NormalAudio(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            special = true;
            trackUI.text = specialText;
            mapUI.sprite = specialMap;
            cam.GetComponent<DigitalGlitch>().intensity = 0.2f;
            winnerUI.sprite = winnerImg[14];
            winnerUI.color = Color.gray;
            winnerName.text = "IDEAL";
            AudioManager.Instance.PlaySFX("Key");
            AudioManager.Instance.GlitchExtraAudio();
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (special) {
                sceneChanger.SwitchScene("Special");
                AudioManager.Instance.NormalAudio(0.1f);
            } else {
                sceneChanger.SwitchScene(sceneName[track - 1]);
            }
            AudioManager.Instance.PlaySFX("Select");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            sceneChanger.SwitchScene("MainMenu");
            AudioManager.Instance.PlaySFX("Cancel");
            AudioManager.Instance.FadeOut(1f);
        }
    }

    void UpdateUI() {
        special = false;
        cam.GetComponent<DigitalGlitch>().intensity = 0.0f;
        trackUI.text = trackName[track - 1];
        mapUI.sprite = mapImg[track - 1];
        if (winners[track - 1] != -1) {
            winnerUI.sprite = winnerImg[winners[track - 1]-1];
            switch (winners[track - 1]) {
                case 1:
                    winnerName.text = "Yi Sang";
                    break;
                case 2:
                    winnerName.text = "Faust";
                    break;
                case 3:
                    winnerName.text = "Don Quixote";
                    break;
                case 4:
                    winnerName.text = "Ryoshu";
                    break;
                case 5:
                    winnerName.text = "Meursault";
                    break;
                case 6:
                    winnerName.text = "Hong Lu";
                    break;
                case 7:
                    winnerName.text = "Heathcliff";
                    break;
                case 8:
                    winnerName.text = "Ishmael";
                    break;
                case 9:
                    winnerName.text = "Rodion";
                    break;
                case 10:
                    winnerName.text = "Sinclair";
                    break;
                case 11:
                    winnerName.text = "Outis";
                    break;
                case 12:
                    winnerName.text = "Gregor";
                    break;
                case 13:
                    winnerName.text = "Ricardo";
                    PlayerPrefs.SetInt(("Sticker" + (track - 1).ToString()), 1);
                    break;
            }
            
        } else {
            winnerUI.sprite = winnerImg[13];
            winnerName.text = "No Data";
            winnerUI.color = Color.gray;
        }
            winnerUI.color = Color.white;
        if (PlayerPrefs.GetInt("Sticker" + (track - 1).ToString()) == 1) {
            sticker.color = new Color(1, 1, 1, 1);
        } else {
            sticker.color = new Color(1, 1, 1, 0);
        }
    }

    void CheckWinner() {
        if (PlayerPrefs.HasKey("Map1")) {
            winners[0] = PlayerPrefs.GetInt("Map1");
        } else { winners[0] = -1; }
        if (PlayerPrefs.HasKey("Map2")) {
            winners[1] = PlayerPrefs.GetInt("Map2");
        } else { winners[1] = -1; }
        if (PlayerPrefs.HasKey("Map3")) {
            winners[2] = PlayerPrefs.GetInt("Map3");
        } else { winners[2] = -1; }
        if (PlayerPrefs.HasKey("Map4")) {
            winners[3] = PlayerPrefs.GetInt("Map4");
        } else { winners[3] = -1; }
        if (PlayerPrefs.HasKey("Map5")) {
            winners[4] = PlayerPrefs.GetInt("Map5");
        } else { winners[4] = -1; }
        if (PlayerPrefs.HasKey("Map6")) {
            winners[5] = PlayerPrefs.GetInt("Map6");
        } else { winners[5] = -1; }
        if (PlayerPrefs.HasKey("Map7")) {
            winners[6] = PlayerPrefs.GetInt("Map7");
        } else { winners[6] = -1; }

    }
}
