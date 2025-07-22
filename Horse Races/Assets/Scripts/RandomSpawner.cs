using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour {
    [SerializeField] GameObject ricardo;
    [SerializeField] GameObject ideal;
    [SerializeField] GameObject sweeperPrefab;

    public bool sweeperActive = false;
    public bool idealActive = false;
    public bool ricardoActive = false;

    public int delay;
    public int sweeperAmount;
    public float sweeperArea;
    public float sweeperTimer = 60f;

    int sweeperCount = 0;
    [Range(1, 3)] public int spawnChance;

    public bool Testing = false;
    int _event = -1;

    int _spawnTimer = 0;
    bool _shouldSpawn = false;

    void Start() {
        InvokeRepeating("Timer", 1f, 1f);
        if (!Testing) {
            SpawnSort();
        } else { Spawner(); }
    }

    void SpawnSort() {
        int chance = Random.Range(1, 12);
        if (chance % spawnChance == 0) {
            _shouldSpawn = true;
        }
    }

    void Spawner() {
        if (_event == -1) {
            _event = Random.Range(1, 6);
        }
        if (GameManager.Instance.pauseActive != GameManager.PauseStatus.Pause) {
            switch (_event) {
                case 1:
                case 4:
                    if (ricardoActive) {
                        ricardo.SetActive(true);
                    } else { ideal.SetActive(true); }
                    break;
                case 3:
                case 6:
                    if (idealActive) {
                        ideal.SetActive(true);
                    } else { ricardo.SetActive(true); }
                    break;
            }
        }
    }
    void sweeperSpawner() {
        if (GameManager.Instance.gameStart) {
            if (GameManager.Instance.pauseActive != GameManager.PauseStatus.Pause) {
                Vector3 pos = gameObject.transform.position + (Random.insideUnitSphere * sweeperArea);
                Instantiate(sweeperPrefab, pos, gameObject.transform.rotation);
                sweeperCount++;
                if (sweeperCount != sweeperAmount) {
                    Invoke("sweeperSpawner", 0.5f);
                }
            }
        }
    }

    void Timer() {
        if (GameManager.Instance.pauseActive != GameManager.PauseStatus.Pause) {
            _spawnTimer++;
            if (_shouldSpawn && _spawnTimer == delay) {
                Spawner();
            }
            if (sweeperActive && _spawnTimer == sweeperTimer) {
                sweeperSpawner();
            }
        }
    }
}
