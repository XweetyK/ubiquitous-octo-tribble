using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [SerializeField] GameObject fadeFlowchart;
    public string nextScene;
    public float delay;
    public bool tutorial = false;

    private void Update() {
        if (tutorial) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SwitchScene(nextScene);
            }
        }
    }

    void SwitchScene() {
        if (nextScene != null) {
            SceneManager.LoadScene(nextScene);
        } else { Debug.Log("Scene name empty at " + " " + gameObject.name); }
    }
    public void SwitchScene(string scene) {
        nextScene = scene;
        fadeFlowchart.SetActive(true);
        Invoke("SwitchScene", delay);
    }
    public void Disable() {
        tutorial = false;
    }
}
