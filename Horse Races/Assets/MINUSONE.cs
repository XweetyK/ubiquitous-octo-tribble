using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MINUSONE : MonoBehaviour
{
    private void Start() {
        if (SceneManager.GetActiveScene().name == "TEST") {
            Cursor.visible = true;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (Input.GetKeyDown(KeyCode.O)) {
                SceneManager.LoadScene("TEST");
            }
        }
    }
}
