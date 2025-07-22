using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiss : MonoBehaviour {
    [SerializeField] GameObject target;
    //[SerializeField] Animator anim;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (target != null) {
            if (collision.gameObject == target) {
                gameObject.transform.Find("Heart").gameObject.SetActive(true);
                target.transform.Find("Heart").gameObject.SetActive(true);
                Invoke("DestroyHeart", 0.3f);
                gameObject.GetComponent<AudioSource>().Play();
                //anim.SetTrigger("Kiss");
            }
        }
    }
    void DestroyHeart() {
        gameObject.transform.Find("Heart").gameObject.SetActive(false);
        if (target != null) {
            target.transform.Find("Heart").gameObject.SetActive(false);
        }
    }
}
