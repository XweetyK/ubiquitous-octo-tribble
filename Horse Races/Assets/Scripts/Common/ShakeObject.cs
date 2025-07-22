using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour {
    public float magnitude;
    bool ActiveShake;

    Vector3 initPos;

    void Start() {
        initPos = gameObject.transform.position;
        ActiveShake = false;
    }

    void Update() {
        if (ActiveShake) {
            gameObject.transform.position = new Vector3(initPos.x + (Random.insideUnitCircle.x * magnitude), initPos.y + (Random.insideUnitCircle.y * magnitude), initPos.z);
        }
    }

    public void Shake(float time) {
        ActiveShake = true;
        initPos = gameObject.transform.position;
        Invoke("StopShake", time);
    }

    void StopShake() {
        ActiveShake = false;
        gameObject.transform.position = initPos;
    }
}
