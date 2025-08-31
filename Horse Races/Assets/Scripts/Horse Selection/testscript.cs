using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    [SerializeField] Sprite[] image;
    [SerializeField] float repeat;

    private void Start() {
        InvokeRepeating("ChangeSprite", 0, repeat);
    }

    void ChangeSprite() {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == image[0]) {
            gameObject.GetComponent<SpriteRenderer>().sprite = image[1];
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = image[0];
        }
    }
}
