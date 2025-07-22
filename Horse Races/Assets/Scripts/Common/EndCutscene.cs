using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] SpriteRenderer cg;
    [SerializeField] Color color;
    [SerializeField] GameObject creditCanvas;
    [SerializeField] GameObject vignette;
    [SerializeField] Animator anim;
    //--
    float t;
    [SerializeField] GameObject CGObj;
    Vector3 initPos;
    Vector3 initScale;
    [SerializeField] Transform target;
    public float timeToReachTarget;

    private void Start() {
        initPos = CGObj.transform.position;
        initScale = CGObj.transform.localScale;
    }

    public void GlitchStart() {
        AudioManager.Instance.PlayBGM("D_Mirror", 2f, 0f);
        AudioManager.Instance.GlitchAudio();
        cg.color = color;
        creditCanvas.SetActive(false);
        vignette.SetActive(true);
        Camera.main.GetComponent<AnalogGlitch>().enabled = true;
        Camera.main.GetComponent<DigitalGlitch>().enabled = true;
    }
    public void GlitchStop() {
        Camera.main.GetComponent<AnalogGlitch>().enabled = false;
        Camera.main.GetComponent<DigitalGlitch>().enabled = false;
    }
    public void FadeCredits() {
        anim.SetTrigger("fade");
    }

    void Update() {
        t += Time.deltaTime / timeToReachTarget;
        CGObj.transform.position = Vector3.Lerp(initPos, target.position, t);
        CGObj.transform.localScale = Vector3.Lerp(initScale, target.localScale, t);
    }
}
