using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class GlitchActivator : MonoBehaviour
{
    public bool Ideal = false;

    private void Start() {
        if (Ideal) {
            StartGlitch();
        }
    }

    void StartGlitch() {
        Camera.main.GetComponent<AnalogGlitch>().enabled = true;
        Camera.main.GetComponent<DigitalGlitch>().enabled = true;
        if (Ideal) {
            AudioManager.Instance.GlitchExtraAudio();
        }
    }
    
    void StopGlitch() {
        Camera.main.GetComponent<AnalogGlitch>().enabled = false;
        Camera.main.GetComponent<DigitalGlitch>().enabled = false;
        if (Ideal) {
            AudioManager.Instance.NormalAudio(0.1f);
        }
    }

    private void OnDestroy() {
        if (Camera.main != null) {
            StopGlitch();
        }
    }
}
