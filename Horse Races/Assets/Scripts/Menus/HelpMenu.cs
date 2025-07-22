using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] Text cathy;
    [SerializeField] Text encounters;
    [SerializeField] GameObject cathyIcon;
    [SerializeField] SceneChanger sceneChanger;
    void Start()
    {
        if (PlayerPrefs.HasKey("ExtraMenu")) {
            cathy.text = "-To replay story mode, you should try clearing the cache (Don't worry, this will not reset your records)";
            encounters.text = "-Ricardo might appear in all the stages\n-Sweepers might appear if the battle royale match lasts too long\n-Ideal :)";
            cathyIcon.SetActive(true);
        } else {
            cathy.text = "-???????-";
            encounters.text = "-???????-";
            cathyIcon.SetActive(false);
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            AudioManager.Instance.PlaySFX("Cancel");
            sceneChanger.SwitchScene("MainMenu");
            Debug.Log("esc");
        }
    }
}
