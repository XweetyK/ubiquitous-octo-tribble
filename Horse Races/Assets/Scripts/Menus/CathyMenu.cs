using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CathyMenu : MonoBehaviour {
    [SerializeField] Text yes;
    [SerializeField] Text cancel;
    int menu = 1;
    MainMenu mainMenu;

    private void Start() {
        UpdateUI();
        mainMenu = GameObject.FindObjectOfType<MainMenu>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            menu = 2;
            UpdateUI();
            AudioManager.Instance.PlaySFX("Key");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            menu = 1;
            UpdateUI();
            AudioManager.Instance.PlaySFX("Key");
        }

        if (Input.GetButtonDown("Submit")) {
            if (menu == 1) {
                AudioManager.Instance.PlaySFX("Cathy");
                mainMenu.ClearCache();
                mainMenu.CathyMenu();
            }
            if (menu == 2) {
                AudioManager.Instance.PlaySFX("Cancel");
                mainMenu.CathyMenu();
            }
        }
    }

    void UpdateUI() {
        if (menu == 1) {
            yes.color = Color.red;
            cancel.color = Color.white;
        } else {
            yes.color = Color.white;
            cancel.color = Color.red;
        }
    }
}
