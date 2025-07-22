using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGoal : MonoBehaviour
{
    [SerializeField] HorseBehavior ideal;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag=="Horse") {
            ideal.gameObject.transform.position = collision.transform.position;
            ideal.IdealOverclock();
        }
    }
}
