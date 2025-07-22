using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    private void Start() {
        if (!PlayerPrefs.HasKey("Jump")) {
            PlayerPrefs.SetString("Jump", "Jump");
        }
    }
}
