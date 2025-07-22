using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicardoSpawner : MonoBehaviour
{
    [SerializeField] GameObject ricardo;
    public bool active;
    void Start()
    {
        if (active) {
            Invoke("Spawn", 15f);
        }
    }

    void Spawn() {
        Vector3 pos = new Vector3(1.45f, 1.45f, 0);
        Instantiate(ricardo, pos, ricardo.transform.rotation);
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
