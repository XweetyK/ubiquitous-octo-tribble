using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kino;

public class Carrot : MonoBehaviour {
    public bool active = true;
    public bool bytesDL = false;
    [SerializeField] GameObject prompt;
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] AnalogGlitch glitch;
    int menu=0;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (active) {
            switch (bytesDL) {
                case true:
                    GameManager.Instance.PauseGame();
                    AudioManager.Instance.GlitchExtraAudio();
                    glitch.enabled = true;
                    Invoke("Dl", 2f);
                    Camera.main.orthographicSize = 2;
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 35);
                    break;
                case false:
                    Time.timeScale = 1;
                    GameManager.Instance.EndGame(collision.gameObject);
                    if (SceneManager.GetActiveScene().name != "Last Race") {
                        AudioManager.Instance.PlaySFX("Win");
                    }
                    if (GameObject.FindObjectOfType<Leaderboard>() != null) {
                        GameObject.FindObjectOfType<Leaderboard>().Win(collision.gameObject.name);
                    }
                    break;
            } 
        }
    }

    private void Dl() {
        prompt.SetActive(true);
    }
    private void Update() {
        if (bytesDL) {
            if (prompt.activeSelf) {
                if (Input.GetButtonDown("Submit")) {
                    AudioManager.Instance.PlaySFX("Select");
                    sceneChanger.SwitchScene("Download");
                }
            }
        }
    }
}
