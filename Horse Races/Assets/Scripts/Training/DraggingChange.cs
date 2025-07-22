using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingChange : MonoBehaviour
{
    [SerializeField] Sprite normal;
    [SerializeField] Sprite pickedUp;
    SpriteRenderer render;

    private void Awake() {
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnDrag() {
        render.sprite = pickedUp;
    }

    public void OnRelease() {
        render.sprite = normal;
    }
}
