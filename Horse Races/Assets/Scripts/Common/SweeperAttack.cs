using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperAttack : MonoBehaviour
{
    public static SweeperAttack Instance { get; private set; }
    List<GameObject> Queue;
    float randTimer;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else { Destroy(gameObject); }
    }

    private void Start() {
        Queue = new List<GameObject>();
        randTimer = Random.Range(1f, 2f);
        Invoke("Attack", randTimer);
        Debug.Log(Queue.Count);
    }

    public void AddToQueue(GameObject s) {
        if (!Queue.Contains(s)) {
            Queue.Add(s);
        }
        Debug.Log(Queue.Count);
    }

    void Attack() {
        if (Queue.Count != 0) {
            Queue[0].GetComponent<HorseBehavior>().Overclock();
            Queue.Remove(Queue[0]);
        }
        randTimer = Random.Range(0.1f, 0.5f);
        Invoke("Attack", randTimer);
    }
}
