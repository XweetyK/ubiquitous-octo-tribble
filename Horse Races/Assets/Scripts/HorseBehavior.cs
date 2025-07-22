using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseBehavior : MonoBehaviour {
    [SerializeField] GameObject _name;
    [SerializeField] public Image WinScreen;
    [SerializeField] GameObject DeathSprite;
    [SerializeField] Color color;
    [SerializeField] GameObject coin;
    [SerializeField] Text coinText;
    [SerializeField] GameObject UIIcon;
    public float Boost = 10;
    float EGOBoost;
    Vector3 dir;
    GameManager GM;
    bool launched = false;
    public bool EGO = false;
    public bool battleRoyale;
    public bool egoActive = true;

    [Range(1, 3)] public int aggro = 1;
    public float egoInitialDelay = 6;
    public float egoRepeatTime = 4;

    public bool YsLastRace = false;
    Vector2 lastDir = Vector2.zero;

    public int CoinNumber = 0;

    void Start() {
        if (GameManager.Instance != null) {
            GM = GameManager.Instance;
        } else { Debug.LogWarning("instance null"); }
        dir = RegenDir();
        if (egoActive) {
            switch (aggro) {
                case 1:
                    InvokeRepeating("CoinChance", Random.Range(egoInitialDelay - 1f, egoInitialDelay + 1f), Random.Range(egoRepeatTime - 1, egoRepeatTime + 1));
                    break;
                case 2:
                    InvokeRepeating("CoinChance", Random.Range(egoInitialDelay - 1f, egoInitialDelay + 1f), Random.Range(egoRepeatTime - 0.5f, egoRepeatTime + 0.5f));
                    break;
                case 3:
                    InvokeRepeating("CoinChance", Random.Range(egoInitialDelay - 1f, egoInitialDelay + 1f), Random.Range(egoRepeatTime - 0.3f, egoRepeatTime + 0.3f));
                    break;
            }
        }
    }

    void Update() {
        if (GM.gameStart) {
            if (!launched) {
                if (_name.activeSelf) { _name.SetActive(false); }
                if (!YsLastRace) {
                    LaunchHorse();
                }
                launched = true;
            } else {
                if (GM.pauseActive != GameManager.PauseStatus.Pause) {

                    gameObject.GetComponent<Rigidbody2D>().velocity =
                        gameObject.GetComponent<Rigidbody2D>().velocity.normalized * (GM.HorseSpeed + EGOBoost);
                }
            }
        }
        if (GM.End()) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (GM.pauseActive == GameManager.PauseStatus.Pause) {
            if (lastDir == Vector2.zero) {
                lastDir = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    //-------------------------COLLISION-----------------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision) {
        if (EGO) {
            if (collision.gameObject.tag == "Horse") {
                collision.gameObject.GetComponent<HorseBehavior>().Death(color,CoinNumber);
                Camera.main.GetComponent<ShakeObject>().Shake(0.2f);
            }
            //if (gameObject.tag == "Ricardo" && collision.gameObject.tag == "Sweeper") {
            //    collision.gameObject.GetComponent<HorseBehavior>().Death(color);
            //}
            if (collision.gameObject.tag == "Ideal") {
                Time.timeScale = 0.3f;
                Camera.main.GetComponent<ShakeObject>().Shake(0.2f);
                collision.gameObject.GetComponent<HorseBehavior>().Death(color,CoinNumber);
                LaunchHorse(GameObject.FindObjectOfType<Carrot>().transform.position);
            }
            coin.SetActive(false);
            CoinNumber = 0;
            EGO = false;
            EGOBoost = 0;
        } else {
            if (!GameManager.Instance.MainMenu) {
                AudioManager.Instance.PlayJump();
            }
        }

        FlipSprite();
    }
    //-------------------------LAUNCH HORSE--------------------------------------------------------------------------------------------------------------------
    void LaunchHorse() {
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * GM.HorseSpeed;
    }
    public void LaunchHorse(Vector3 target) {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.GetComponent<Rigidbody2D>().velocity = (target - transform.position).normalized * GM.HorseSpeed;
    }

    Vector3 RegenDir() {
        Vector3 vec = Vector3.Normalize(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0));
        return vec;
    }
    //-------------------------EGO-----------------------------------------------------------------------------------------------------------------------------
    void CoinChance() {
        if (!GM.End() && this.gameObject.name != "Yi Sang") {
            int chance = Random.Range(0, 6);
            switch (aggro) {
                case 1:
                    if (battleRoyale) {
                        if (chance > 3) {
                            CoinFlip();
                        }
                    } else {
                        if (chance == 5) {
                            CoinFlip();
                        }
                    }
                    break;
                case 2:
                    if (battleRoyale) {
                        if (chance % 2 == 0) {
                            CoinFlip();
                        }
                    } else {
                        if (chance > 3) {
                            CoinFlip();
                        }
                    }
                    break;
                case 3:
                    CoinNumber = 99;
                    Invoke("SweeperQueue", 1f);
                    break;
            }
        }
    }

    void CoinFlip() {
        if (GM.pauseActive != GameManager.PauseStatus.Pause) {
            AudioManager.Instance.PlaySFX("ActiveCoin");
            coinText.text = " ";
            coin.SetActive(true);
            if (aggro == 1) {
                if (battleRoyale) {
                    if (EGO == false && Random.Range(0, 6) > 2) {
                        CoinNumber = Random.Range(1, 11);
                        Invoke("HitHorse", 1f);
                    } else { Invoke("BadCoin", 2f); }

                } else {
                    if (EGO == false && Random.Range(0, 5) == 2) {
                        CoinNumber = Random.Range(1, 11);
                        Invoke("HitHorse", 1f);
                    } else { Invoke("BadCoin", 2f); }
                }
            }
            if (aggro == 2) {
                if (battleRoyale) {
                    CoinNumber = 99;
                    Invoke("HitHorse", 1f);
                } else {
                    if (EGO == false && (Random.Range(0, 6) % 2) == 0) {
                        CoinNumber = 99;
                        Invoke("HitHorse", 1f);
                    } else { Invoke("BadCoin", 2f); }
                }
            }
        }

    }
    void BadCoin() {
        if (GM.pauseActive != GameManager.PauseStatus.Pause) {
            coin.SetActive(false);
            AudioManager.Instance.PlaySFX("BadCoin");
        }
    }

    Vector3 FindHorses() {
        HorseBehavior[] horses = new HorseBehavior[GameObject.FindObjectsOfType<HorseBehavior>().Length];
        horses = GameObject.FindObjectsOfType<HorseBehavior>();
        float dist = 9999;
        Vector3 closest = Vector2.zero;

        for (int i = 0; i < horses.Length; i++) {
            if (horses[i].gameObject != this.gameObject && Vector3.Distance(this.transform.position, horses[i].transform.position) < dist) {
                dist = Vector3.Distance(this.transform.position, horses[i].transform.position);
                closest = horses[i].transform.position;
            }
        }
        return closest;
    }

    void HitHorse() {
        if (GM.pauseActive != GameManager.PauseStatus.Pause && !GameManager.Instance.End()) {
            coinText.text = CoinNumber.ToString();
            coin.GetComponent<Image>().color = Color.yellow;
            coin.GetComponent<Animator>().StopPlayback();
            AudioManager.Instance.PlaySFX("Coin");
            if (GameObject.FindObjectsOfType<HorseBehavior>().Length > 1) {
                gameObject.GetComponent<Rigidbody2D>().velocity = (FindHorses() - transform.position).normalized * GM.HorseSpeed;
                EGOBoost = Boost;
                EGO = true;
            }
        }
    }

    public void Overclock() {
        CoinNumber = 10;
        HitHorse();
    }
    public void IdealOverclock() {
        CoinNumber = 10;
        HitHorse();
        LaunchHorse(GameObject.FindObjectOfType<Carrot>().transform.position);
    }
    void SweeperQueue() {
        SweeperAttack.Instance.AddToQueue(this.gameObject);
    }
    //-------------------------DEATH----------------------------------------------------------------------------------------------------------------------------
    public void Death(Color c, int coin) {
        if (CoinNumber < coin) {
            GameObject dead = Instantiate(DeathSprite, transform.position, transform.rotation);
            dead.GetComponent<SpriteRenderer>().color = color;
            dead.transform.Find("Killer").gameObject.GetComponent<SpriteRenderer>().color = c;
            if (UIIcon != null) {
                UIIcon.GetComponent<ShakeObject>().Shake(0.5f);
                UIIcon.GetComponent<FadeColor>().StartFade(2f);
            }
            if (battleRoyale && UIIcon != null) {
                GameObject.FindObjectOfType<BattleRoyale>().RemoveHorse(UIIcon);
            }
            Destroy(this.gameObject);
        }
    }

    void FlipSprite() {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void ResumeSpeed() {
        gameObject.GetComponent<Rigidbody2D>().velocity = lastDir;
        lastDir = Vector2.zero;
        if (coin.activeSelf) {
            CoinFlip();
        }
    }
}
