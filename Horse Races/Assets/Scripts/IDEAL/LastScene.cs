using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastScene : MonoBehaviour {
    [SerializeField] HorseBehavior ys;
    [SerializeField] HorseBehavior ideal;
    [SerializeField] SpriteRenderer[] sprite;

    [SerializeField] Transform ysPos;
    [SerializeField] Transform idealPos;
    [SerializeField] Transform goalPos;

    [SerializeField] Image end;
    bool activeCG { get; set; }

    public bool test;

    private void Update() {
        if (activeCG) {
            end.color = new Color(255f, 255f, 255f, Mathf.Lerp(end.color.a, 1, 2 * Time.deltaTime));
        }
    }

    void LaunchYS() {
        Vector2 dir = ys.transform.position + Vector3.right;
        sprite[0].sprite = sprite[1].sprite;
        sprite[1].gameObject.SetActive(false);
        sprite[0].flipX = !sprite[0].flipX;
        ys.LaunchHorse(dir);

        ideal.transform.position = new Vector3(100, 100, 0);
    }

    void MoveYS() {
        ys.transform.position = new Vector3(100, 100, 0);
    }

    void TeleportIdeal() {
        ideal.transform.position = idealPos.position;
        ideal.LaunchHorse(goalPos.position);
    }

    void TeleportYS() {
        ys.transform.position = ysPos.position;
        ys.Overclock();
    }

    public void CG() {
        Invoke("StartCG", 3f);
    }

    void StartCG() {
        activeCG = true;
    }
}
