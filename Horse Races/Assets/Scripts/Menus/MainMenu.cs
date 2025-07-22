using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] SpriteRenderer Horse;
    [SerializeField] Sprite YiSang;
    [SerializeField] Sprite Ideal;
    [SerializeField] GameObject Cathy;
    //----UI---------------------------------
    [SerializeField] Text[] buttons;
    [SerializeField] GameObject settingsMenuUI;
    [SerializeField] GameObject cathyMenuUI;
    [SerializeField] GameObject QuitFade;
    int menu = 0;
    //----Sounds-----------------------------
    [SerializeField] SettingsMenu settings;
    [SerializeField] GameObject popup;

    bool cathyMenuOpen = false;
    bool settingsMenuOpen = false;
    bool extraUnlocked = false;

    private void Awake() {
        Cursor.visible = false;
        if (PlayerPrefs.HasKey("Popup")) {
            popup.SetActive(false);
        }
    }

    private void Start() {
        settings.LoadAudio();
        AudioManager.Instance.NormalAudio(0.1f);
        AudioManager.Instance.PlayBGM("MainMenu", 2f, 0);
        if (GlobalVar.Instance.IdealComplete) {
            Horse.sprite = YiSang;
            Cathy.SetActive(true);
        } else {
            Cathy.SetActive(false);
            Horse.sprite = Ideal;
        }
        if (PlayerPrefs.HasKey("ExtraMenu")) {
            extraUnlocked = true;
        }
        UpdateUI();

        if (PlayerPrefs.GetString("Jump") == "Jump") {
            InvokeRepeating("StartAnim", 0.2f, 0.2f);
        }
    }

    private void Update() {
        if (PlayerPrefs.HasKey("Popup")) {
            if (Input.GetKey(KeyCode.R)) {
                PlayerPrefs.DeleteAll();
                UpdateUI();
                AudioManager.Instance.PlayJump();
            }
            if (PlayerPrefs.GetString("Story") == "PracticeScene") {
                PlayerPrefs.DeleteKey("Story");
                UpdateUI();
            }
            if (!cathyMenuOpen && !settingsMenuOpen) {
                if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    if (!Cathy.activeSelf) {
                        if (menu != buttons.Length - 1) {
                            menu++;
                            AudioManager.Instance.PlaySFX("Key");
                        }
                    } else {
                        if (menu != buttons.Length) {
                            menu++;
                            AudioManager.Instance.PlaySFX("Key");
                        }
                    }
                    UpdateUI();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    if (menu != 0) {
                        menu--;
                        AudioManager.Instance.PlaySFX("Key");
                    }
                    UpdateUI();
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    menu = 5;
                    AudioManager.Instance.PlaySFX("Key");
                    UpdateUI();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    menu = 6;
                    AudioManager.Instance.PlaySFX("Key");
                    UpdateUI();
                }
            }

            if (Input.GetButtonDown("Submit")) {
                if (!settingsMenuOpen && !cathyMenuOpen) {
                    Enter();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (settingsMenuOpen) {
                    SettingsMenu();
                    AudioManager.Instance.PlaySFX("Cancel");
                }
                if (cathyMenuOpen) {
                    CathyMenu();
                    AudioManager.Instance.PlaySFX("Cancel");
                }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Return)) {
                AudioManager.Instance.PlaySFX("Select");
                popup.SetActive(false);
                PlayerPrefs.SetString("Popup", "yes");
            }
        }

    }

    void Enter() {
        switch (menu) {
            case 0:
                StartButton();
                buttons[0].color = Color.red;
                AudioManager.Instance.PlaySFX("Select");
                break;
            case 1:
                buttons[1].color = Color.red;
                SettingsMenu();
                AudioManager.Instance.PlaySFX("Select");
                break;
            case 2:
                if (extraUnlocked) {
                    gameObject.GetComponent<SceneChanger>().SwitchScene("PracticeScene");
                    buttons[2].color = Color.red;
                AudioManager.Instance.PlaySFX("Select");
                } else {
                    buttons[2].color = Color.gray;
                AudioManager.Instance.PlaySFX("Wrong");
                }
                break;
            case 3:
                if (extraUnlocked) {
                    gameObject.GetComponent<SceneChanger>().SwitchScene("Jukebox");
                    buttons[3].color = Color.red;
                AudioManager.Instance.PlaySFX("Select");
                } else {
                    buttons[3].color = Color.gray;
                AudioManager.Instance.PlaySFX("Wrong");
                }
                break;
            case 4:
                buttons[4].color = Color.red;
                AudioManager.Instance.PlaySFX("Select");
                QuitFade.SetActive(true);
                break;
            case 5:
                gameObject.GetComponent<SceneChanger>().SwitchScene("Hel");
                buttons[5].color = Color.red;
                break;
            case 6:
                Cathy.GetComponent<Image>().color = Color.yellow;
                CathyMenu();
                AudioManager.Instance.PlaySFX("Select");
                break;
        }
    }

    void UpdateUI() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].color = Color.white;
        }
        Cathy.GetComponent<Image>().color = Color.gray;
        if (!extraUnlocked) {
            buttons[2].text = "-???-";
            buttons[2].color = Color.gray;
            buttons[3].text = "-???-";
            buttons[3].color = Color.gray;
        } else {
            buttons[2].text = "-extra story-";
            buttons[3].text = "-jukebox-";
        }

        if (PlayerPrefs.HasKey("Story")) {
            buttons[0].text = "-continue-";
        } else {
            if (GlobalVar.Instance.IdealComplete) {
                buttons[0].text = "-SELECT STAGE-";
            } else {
                buttons[0].text = "-START!-";
            }
        }
        if (menu != 6) {
            buttons[menu].color = Color.yellow;
        } else {
            Cathy.GetComponent<Image>().color = Color.white;
        }
    }

    void StartButton() {
        if (GlobalVar.Instance.IdealComplete) {
            if (PlayerPrefs.GetString("Jump") == "Jump") {
                gameObject.GetComponent<SceneChanger>().SwitchScene("Jumpscare");
                PlayerPrefs.SetString("Jump", "Done");
            } else {
                gameObject.GetComponent<SceneChanger>().SwitchScene("StageSelect");
            }
        } else {
            if (PlayerPrefs.HasKey("Story")) {
                gameObject.GetComponent<SceneChanger>().SwitchScene(PlayerPrefs.GetString("Story"));
            } else {
                gameObject.GetComponent<SceneChanger>().SwitchScene("Tutorial");
            }
        }
        AudioManager.Instance.FadeOut(1f);
    }

    public void ClearCache() {
        GlobalVar.Instance.IdealComplete = false;
        PlayerPrefs.SetInt("Ideal", 0);
        PlayerPrefs.DeleteKey("Story");
        Cathy.SetActive(false);
        Horse.sprite = Ideal;
        menu = 0;
        UpdateUI();
    }

    public void CathyMenu() {
        if (!cathyMenuOpen) {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(false);
            }
            cathyMenuUI.SetActive(true);
            cathyMenuOpen = true;
        } else {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(true);
            }
            cathyMenuUI.SetActive(false);
            UpdateUI();
            cathyMenuOpen = false;
        }
    }

    public void SettingsMenu() {
        if (!settingsMenuOpen) {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(false);
            }
            settingsMenuUI.SetActive(true);
            settingsMenuOpen = true;
        } else {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].gameObject.SetActive(true);
            }
            settingsMenuUI.SetActive(false);
            UpdateUI();
            settingsMenuOpen = false;
        }
    }

    void StartAnim() {
        int rand = Random.Range(1, 4);
        switch (rand) {
            case 1:
                buttons[0].text = "-SELECT STAGE-";
                break;
            case 2:
                buttons[0].text = "-continue-";
                break;
            case 3:
                buttons[0].text = "-START!-";
                break;
        }
    }
}
