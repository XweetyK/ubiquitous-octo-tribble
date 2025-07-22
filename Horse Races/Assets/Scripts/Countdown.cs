using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] GameObject[] countdownUI;
    int countdown = 0;

    private void Start() {
        Invoke("CountdownStart", 1f);
    }

    void CountdownStart() {
        if (countdown != 3) {
            countdownUI[countdown].SetActive(true);
            countdown++;
            Invoke("CountdownStart", 1f);
        }
    }
}
