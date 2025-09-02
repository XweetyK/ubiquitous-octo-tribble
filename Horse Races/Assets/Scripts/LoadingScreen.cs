using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Text flavorText;
    [SerializeField] SceneChanger sceneChanger;
    string[] messages;
    int randomText;
    public bool idealEnd = false;

    private void Start() {
        AudioManager.Instance.FadeOut(5f);
        if (idealEnd) {
            if (GlobalVar.Instance != null) {
                GlobalVar.Instance.IdealComplete = true;
            }
            PlayerPrefs.SetInt("Ideal", 1);
            PlayerPrefs.SetInt("ExtraMenu", 1);
            PlayerPrefs.DeleteKey("Story");
        }
        randomText = Random.Range(1, 5);
        switch (randomText) {
            case 1:
                flavorText.text = messages[0];//"Horses can run at horse speed.";
                break;
            case 2:
                flavorText.text = messages[1];//"Demian is in the code but he does nothing\n(or so we think).";
                break;
            case 3:
                flavorText.text = messages[2];//"You're never safe from a horse.";
                break;
            case 4:
                flavorText.text = messages[3];//"Press UP for a surprise (no, not here).";
                break;
        }
    }

    private void Update() {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, 1f, 0.5f * Time.deltaTime);
        if (bar.fillAmount>0.98f) {
            sceneChanger.SwitchScene(sceneChanger.nextScene);
        }
    }

    public void SetLanguage(string[] messageTL) {
        messages = messageTL;
    }
}
