using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kino;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject flowchart;
    [SerializeField] GameObject flowEnd;
    [SerializeField] SpriteRenderer forest;
    [SerializeField] Color darkColor;
    [SerializeField] GameObject vignette;
    bool darkforest = false;
    bool darkestforest = false;
    public bool MidGameD = false;
    public float MidDelay = 7f;

    private void Start() {
        if (MidGameD) {
            Invoke("EnableFlowM3", MidDelay);
        }
    }

    private void Update() {
        if (darkforest && !darkestforest) {
            Vector4 vec = Vector4.Lerp(forest.color, darkColor, 0.5f * Time.deltaTime);
            forest.color = new Color(vec.x,vec.y,vec.z,vec.w);
        }
        if (darkestforest) {
            Vector4 vec = Vector4.Lerp(forest.color, Color.black, 0.5f * Time.deltaTime);
            forest.color = new Color(vec.x,vec.y,vec.z,vec.w);
        }

        //if (GameManager.Instance.ActiveCG() && flowEnd != null) {
        //    if (!flowEnd.activeSelf) {
        //        Invoke("EnableFlowEnd", 3f);
        //    }
        //}
    }

    public void EnableFlowM3() {
        flowchart.SetActive(true);
    }
    public void EnableFlowG3() {
        flowchart.SetActive(true);
        Camera.main.GetComponent<AnalogGlitch>().enabled = false;
    }
    public void EnableFlowEnd() {
        flowEnd.SetActive(true);
    }

    public void TurnOffCG() {
        GameManager.Instance.TurnOffCG();
        vignette.SetActive(true);
    }

    public void DarkForest() {
        darkforest = true;
    }
    public void DarkestForest() {
        darkforest = true;
    }
}
