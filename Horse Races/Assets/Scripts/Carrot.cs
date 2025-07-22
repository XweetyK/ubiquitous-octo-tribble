using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carrot : MonoBehaviour {
    public bool active = true;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (active) {
            Time.timeScale = 1;
            GameManager.Instance.EndGame(collision.gameObject);
            if (SceneManager.GetActiveScene().name != "Last Race") {
                AudioManager.Instance.PlaySFX("Win");
            }
            if (GameObject.FindObjectOfType<Leaderboard>() != null) {
                GameObject.FindObjectOfType<Leaderboard>().Win(collision.gameObject.name);
            }
        }
    }

}
