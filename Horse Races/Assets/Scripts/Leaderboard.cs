using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {
    void SetWinner(int winner) {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, winner);
    }

    public void Win(string horse) {
        switch (horse) {
            case "Ideal":
                SetWinner(1);
                break;
            case "Faust":
                SetWinner(2);
                break;
            case "Don":
                SetWinner(3);
                break;
            case "Ryoshu":
                SetWinner(4);
                break;
            case "Meursault":
                SetWinner(5);
                break;
            case "Hong Lu":
                SetWinner(6);
                break;
            case "Heath":
                SetWinner(7);
                break;
            case "Ishmael":
                SetWinner(8);
                break;
            case "Rodion":
                SetWinner(9);
                break;
            case "Sinclair":
                SetWinner(10);
                break;
            case "Outis":
                SetWinner(11);
                break;
            case "Gregor":
                SetWinner(12);
                break;
            case "IdealNew":
                SetWinner(-1);
                break;
            case "RicardoNew":
                SetWinner(13);
                break;
        }
    }
}
