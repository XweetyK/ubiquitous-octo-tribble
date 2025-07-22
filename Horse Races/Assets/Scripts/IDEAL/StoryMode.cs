using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMode : MonoBehaviour {
    DialogueManager flowchart;

    private void Start() {
        flowchart = GameObject.FindObjectOfType<DialogueManager>();
    }

    private void Update() {
        if (GameManager.Instance.ActiveCG()) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                switch (SceneManager.GetActiveScene().name) {
                    case "Ideal 3":
                        flowchart.EnableFlowM3();
                        break;
                    case "GP3":
                        flowchart.EnableFlowG3();
                        break;
                    case "Last Race":
                        flowchart.EnableFlowEnd();
                        break;
                }
            }
        }
    }
}
