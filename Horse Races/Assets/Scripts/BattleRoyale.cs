using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRoyale : MonoBehaviour {

    [SerializeField] Sprite[] numbers;
    [SerializeField] GameObject map;
    public float shrinkDelay;
    public float shrinkSpeed;
    int horsecount = 12;
    bool startShrink = false;

    private void Start() {
        Invoke("StartShrinking", shrinkDelay);
        AudioManager.Instance.PlayBGM("G_BattleRoyale", 1f, 0f);
    }

    private void Update() {
        if (GameManager.Instance.pauseActive != GameManager.PauseStatus.Pause && GameManager.Instance.gameStart) {
            if (startShrink) {
                map.transform.localScale = Vector3.Lerp(map.transform.localScale, new Vector3(1.2f, 1.2f, 1f), shrinkSpeed * Time.deltaTime);
            }
        }
    }

    public void RemoveHorse(GameObject horseUI) {
        horsecount--;
        horseUI.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = numbers[horsecount];
        Invoke("CheckWin", 0.1f);
    }
    void CheckWin() {
        if (horsecount == 1) {
            GameObject winner = GameObject.FindGameObjectWithTag("Horse");
            GameManager.Instance.EndGame(winner);
            AudioManager.Instance.PlaySFX("Win");
            if (GameObject.FindObjectOfType<Leaderboard>() != null) {
                GameObject.FindObjectOfType<Leaderboard>().Win(winner.name);
            }
        }
    }

    void StartShrinking() {
        startShrink = true;
    }
}
