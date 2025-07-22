using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCG : MonoBehaviour
{
    [SerializeField] Sprite[] cg;

    private void Start() {
        int random = Random.Range(0, (cg.Length-1));
        gameObject.GetComponent<SpriteRenderer>().sprite = cg[random];
    }
}
