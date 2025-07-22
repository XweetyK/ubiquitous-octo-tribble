using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] GameObject flow;

    public void Activate() {
        flow.SetActive(true);
    }
}
