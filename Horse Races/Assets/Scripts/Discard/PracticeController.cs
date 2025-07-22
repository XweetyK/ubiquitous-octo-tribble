using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeController : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            sceneChanger.SwitchScene("MainMenu");
        }
    }
}
