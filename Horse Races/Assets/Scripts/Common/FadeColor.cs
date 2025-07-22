using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeColor : MonoBehaviour {
    [SerializeField] Color color;
    bool activeFade;
    [SerializeField]float time;

    void Fade(float time) { }

    private void Update() {
        if (activeFade) {
            gameObject.GetComponent<SpriteRenderer>().color = Vector4.Lerp(gameObject.GetComponent<SpriteRenderer>().color, color, time * Time.deltaTime);
        }
    }

    public void StartFade(float t) {
        time = t;
        activeFade = true;
    }
    public void StartFadeSimple() {
        activeFade = true;
    }
}
