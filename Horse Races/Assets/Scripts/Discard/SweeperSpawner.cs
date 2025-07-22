using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperSpawner : MonoBehaviour {
    [SerializeField] GameObject Sweeper;
    public int limit = 50;
    bool flip = false;
    int count = 0;
    private void Start() {
        InvokeRepeating("Spawn", 5f, 0.1f);
    }

    void Spawn() {
        if (count < limit) {
            float x = Random.Range(-3, 3);
            float y = Random.Range(-3, 3);
            GameObject sweeper;
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            sweeper = Instantiate(Sweeper, pos, transform.rotation);
            if (flip) {
                sweeper.GetComponent<SpriteRenderer>().flipX = true;
            }
            flip = !flip;
            count++;
        }
    }
}
