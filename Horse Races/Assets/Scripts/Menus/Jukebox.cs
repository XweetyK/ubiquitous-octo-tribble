using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jukebox : MonoBehaviour
{
    [SerializeField] Track[] track;
    [SerializeField] Text Name;
    [SerializeField] Text Artist;
    [SerializeField] Image Icon;
    [SerializeField] Sprite[] sprite;
    [SerializeField] GameObject message;
    int select=0;
    bool pause = false;

    private void Start() {
        AudioManager.Instance.PlayBGM(track[select].code, 1f, 1f);
    }

    private void Update() {
        Name.text = track[select].name;
        Artist.text = track[select].artist;

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (select != track.Length-1) {
                select++;
            } else { select = 0; }
            AudioManager.Instance.PlayBGM(track[select].code, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (select != 0) {
                select--;
            } else { select = track.Length-1; }
            AudioManager.Instance.PlayBGM(track[select].code, 1f, 1f);
        }

        if (Input.GetButtonDown("Submit")) {
            if (!pause) {
                pause = true;
                AudioManager.Instance.FadeOut(2f);
                AudioManager.Instance.PlaySFX("Cancel");
            } else {
                pause = false;
                AudioManager.Instance.PlayBGM(track[select].code, 1f,1f);
                AudioManager.Instance.PlaySFX("Select");
            }
        }

        if (track[select].code == "Ricardo") {
            Icon.sprite = sprite[1];
            message.SetActive(true);
        } else {
            Icon.sprite = sprite[0];
            message.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject.FindObjectOfType<SceneChanger>().SwitchScene("MainMenu");
        }
    }
}
