using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YiSangling : MonoBehaviour
{
    [SerializeField] GameObject _name;
    [SerializeField] GameObject DeathSprite;
    [SerializeField] Color color;
    //[SerializeField] GameObject coin;
    public float Boost = 10;
    float EGOBoost;
    Vector3 dir;
    GameManager GM;
    bool launched = false;
    //public bool EGO = false;
    //Vector2 target;

    void Start() {
        if (GameManager.Instance != null) {
            GM = GameManager.Instance;
        } else { Debug.LogWarning("instance null"); }
        dir = RegenDir();
        //InvokeRepeating("CoinChance", 5f, 3f);
    }

    void Update() {
        if (GM.gameStart) {
            if (!launched) {
                if (_name.activeSelf) { _name.SetActive(false); }
                LaunchHorse();
                launched = true;
            }
            //else {
            //    gameObject.GetComponent<Rigidbody2D>().velocity =
            //        gameObject.GetComponent<Rigidbody2D>().velocity.normalized * (GM.HorseSpeed + EGOBoost);
            //}
        }
        if (GM.End()) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    //-------------------------COLLISION-----------------------------------------------------------------------------------------------------------------------
    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (EGO) {
    //        if (collision.gameObject.tag == "Horse") {
    //            collision.gameObject.GetComponent<HorseBehavior>().Death(color);
    //        }
    //        if (gameObject.tag == "Ricardo" && collision.gameObject.tag == "Sweeper") {
    //            collision.gameObject.GetComponent<HorseBehavior>().Death(color);
    //        }
    //        coin.SetActive(false);
    //        EGO = false;
    //        EGOBoost = 0;
    //    }
    //}
    //-------------------------LAUNCH HORSE--------------------------------------------------------------------------------------------------------------------
    void LaunchHorse() {
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * ((GM.HorseSpeed+Random.Range(0.5f, 1f)) /Random.Range(1.5f,2.5f));
    }

    Vector3 RegenDir() {
        Vector3 vec = Vector3.Normalize(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0));
        return vec;
    }
    //-------------------------EGO-----------------------------------------------------------------------------------------------------------------------------
    //void CoinChance() {
    //    if (!GM.End()) {
    //        int chance = Random.Range(0, 10);
    //        if (chance == 5) {
    //            CoinFlip();
    //        }
    //    }
    //}

    //void CoinFlip() {
    //    coin.SetActive(true);
    //    if (EGO == false && Random.Range(0, 5) == 2) {
    //        Invoke("HitHorse", 1f);
    //    } else { Invoke("BadCoin", 1f); }
    //}
    //void BadCoin() {
    //    coin.SetActive(false);
    //}

    //Vector3 FindHorses() {
    //    HorseBehavior[] horses = new HorseBehavior[GameObject.FindObjectsOfType<HorseBehavior>().Length];
    //    horses = GameObject.FindObjectsOfType<HorseBehavior>();
    //    float dist = 9999;
    //    Vector3 closest = Vector2.zero;

    //    for (int i = 0; i < horses.Length; i++) {
    //        if (horses[i].gameObject != this.gameObject && Vector3.Distance(this.transform.position, horses[i].transform.position) < dist) {
    //            dist = Vector3.Distance(this.transform.position, horses[i].transform.position);
    //            closest = horses[i].transform.position;
    //        }
    //    }
    //    return closest;
    //}

    //void HitHorse() {
    //    coin.GetComponent<SpriteRenderer>().color = Color.yellow;
    //    coin.GetComponent<Animator>().StopPlayback();
    //    if (GameObject.FindObjectsOfType<HorseBehavior>().Length > 1) {
    //        gameObject.GetComponent<Rigidbody2D>().velocity = (FindHorses() - transform.position).normalized * GM.HorseSpeed;
    //        EGOBoost = Boost;
    //        EGO = true;
    //    }
    //}

    //public void Overclock() {
    //    HitHorse();
    //}
    //-------------------------DEATH----------------------------------------------------------------------------------------------------------------------------
    public void Death(Color c) {
        GameObject dead = Instantiate(DeathSprite, transform.position, transform.rotation);
        dead.GetComponent<SpriteRenderer>().color = color;
        dead.transform.Find("Killer").
            gameObject.GetComponent<SpriteRenderer>().color = c;
        Destroy(this.gameObject);
    }
}
