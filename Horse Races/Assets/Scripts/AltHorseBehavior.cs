using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltHorseBehavior : MonoBehaviour {
    [SerializeField] GameObject _name;
    [SerializeField] public Image Color;
    Vector3 dir;
    Collider2D col;
    GameManager GM;
    bool launched = false;

    void Start() {
        if (GameManager.Instance != null) {
            GM = GameManager.Instance;
        } else { Debug.LogWarning("instance null"); }
        dir = RegenDir();
        //col = gameObject.GetComponent<Collider2D>();
    }

    void Update() {
        if (GM.gameStart) {
            if (!launched) {
                if (_name.activeSelf) { _name.SetActive(false); }
                LaunchHorse();
                launched = true;
            } else {
                gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized * GM.HorseSpeed;
            }
        }
        if (GM.End()) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (!GameManager.Instance.MainMenu) {
            AudioManager.Instance.PlayJump();
        }
    }

    void LaunchHorse() {
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * GM.HorseSpeed;
    }

    Vector3 RegenDir() {
        Vector3 vec = Vector3.Normalize(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0));
        return vec;
    }
}
