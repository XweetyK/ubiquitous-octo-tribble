using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum PauseStatus { Init, Pause, Unpaused};

    public static GameManager Instance { get; private set; }
    [SerializeField] public bool gameStart;
    [SerializeField] float HSpeed = 2;
    [SerializeField] float FFW;
    public float HorseSpeed;
    private bool endgame = false;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject Gate;
    [SerializeField] GameObject FastIcon;
    GameObject Winner;
    Vector3 WinnerPos;
    bool CG = false;
    [SerializeField] float speed;
    public bool goNextEnter;
    public bool Dialogue { get; set; }
    public bool MainMenu = false;
    [SerializeField] GameObject nextPrompt;
    public PauseStatus pauseActive { get; private set; }

    bool interruptedStart = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else { Destroy(gameObject); }
        gameStart = false;
        HorseSpeed = HSpeed;
        if (!MainMenu) {
            Invoke("StartGame", 4f);
        } else { StartGame(); }
        Dialogue = false;
        pauseActive = PauseStatus.Init;
    }

    private void StartGame() {
        if (pauseActive != PauseStatus.Pause) {
            gameStart = true;
            Gate.SetActive(false);
            if (!MainMenu) {
                InvokeRepeating("IncreaseSpeed", 4f, 3f);
            }
        } else {interruptedStart = true; }
    }

    public void EndGame(GameObject horse) {
        horse.GetComponent<PolygonCollider2D>().enabled = false;
        gameStart = false;
        Winner = horse;
        endgame = true;
        WinnerPos = new Vector3(Winner.transform.position.x, Winner.transform.position.y, Camera.transform.position.z);
        if (SceneManager.GetActiveScene().name != "Last Race") {
            Invoke("EndCG", 2f);
            Invoke("Next", 3f);
            AudioManager.Instance.FadeOut(1f);
            
        } else {
            Invoke("YSCG", 0.5f);
            Invoke("Next", 4.5f);
        }
    }

    private void Update() {
        if (endgame) {
            if (!Dialogue) {
                if (SceneManager.GetActiveScene().name != "Last Race") {
                    CamZoom(WinnerPos, 1f);
                }
            }
        }

        if (Input.GetKey(KeyCode.Space) && !MainMenu) {
            if (SceneManager.GetActiveScene().name != "Last Race") {
                HorseSpeed = HSpeed + FFW;
            }
            FastIcon.SetActive(true);
        } else {
            HorseSpeed = HSpeed;
            FastIcon.SetActive(false);
        }

        if (CG) {
            if (!Dialogue) {
                if (Winner.GetComponent<HorseBehavior>() != null) {
                    Winner.GetComponent<HorseBehavior>().WinScreen.color =
                        new Color(255f, 255f, 255f, Mathf.Lerp(Winner.GetComponent<HorseBehavior>().WinScreen.color.a, 1, speed * Time.deltaTime));
                    if (Winner.GetComponent<HorseBehavior>().WinScreen.color.a == 1) {
                        Dialogue = true;
                    }
                } else {
                    Winner.GetComponent<AltHorseBehavior>().Color.color =
                        new Color(255f, 255f, 255f, Mathf.Lerp(Winner.GetComponent<AltHorseBehavior>().Color.color.a, 1, speed * Time.deltaTime));
                    if (Winner.GetComponent<AltHorseBehavior>().Color.color.a == 1) {
                        Dialogue = true;
                    }
                }
            }

            if (goNextEnter) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    gameObject.GetComponent<SceneChanger>().SwitchScene(gameObject.GetComponent<SceneChanger>().nextScene);
                }
            }
        }
    }

    void EndCG() {
        CG = true;
    }

    void YSCG() {
        CG = true;
        GameObject.FindObjectOfType<LastScene>().CG();
    }

    void Next() {
        nextPrompt.SetActive(true);
        if (SceneManager.GetActiveScene().name != "Last Race") {
            AudioManager.Instance.PlayBGM("G_Win", 0.2f, 1f);
        }
    }

    void IncreaseSpeed() {
        if (pauseActive != PauseStatus.Pause) {
            HSpeed += 0.2f;
        }
    }

    public bool End() { return endgame; }
    public bool ActiveCG() { return CG; }

    public void TurnOffCG() {
        Dialogue = true;
        nextPrompt.SetActive(false);
        // Winner.GetComponent<HorseBehavior>().WinScreen.gameObject.SetActive(false);
        Winner.GetComponent<HorseBehavior>().WinScreen.transform.parent.gameObject.SetActive(false);
    }

    public void CamZoom(Vector3 pos, float zoom) {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, pos, (speed + 0.5f) * Time.deltaTime);
        Camera.GetComponent<Camera>().orthographicSize =
            Mathf.Lerp(Camera.GetComponent<Camera>().orthographicSize, zoom, speed * Time.deltaTime);
    }
    public void CamFollow(Vector3 pos) {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, pos, (speed + 0.5f) * Time.deltaTime);
    }

    public void PauseGame (){
        switch (pauseActive) {
            case PauseStatus.Init:
            case PauseStatus.Unpaused:
                pauseActive = PauseStatus.Pause;
                break;

            case PauseStatus.Pause:
                pauseActive = PauseStatus.Unpaused;
                HorseBehavior[] horses = GameObject.FindObjectsOfType<HorseBehavior>();
                foreach (var horse in horses) {
                    horse.ResumeSpeed();
                }
                if (interruptedStart) {
                    Invoke("StartGame", 1f);
                }
                break;
        }
    }
}
