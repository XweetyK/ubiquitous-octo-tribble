using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    float t;
    Vector3 startPosition;
    [SerializeField] Transform target;
    public float timeToReachTarget;

    void Start() {
        startPosition = gameObject.transform.position;
        t = 0;
    }

    void Update() {
        t += Time.deltaTime / timeToReachTarget;
        gameObject.transform.position = Vector3.Lerp(startPosition, target.position, t);
    }
}
