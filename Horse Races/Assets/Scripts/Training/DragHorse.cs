using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHorse : MonoBehaviour
{
    GameObject pickedUp;
    Camera cam;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 horseOffset;
    Vector2 hotspot;
    [SerializeField] Sprite[] cursor;
    [SerializeField] GameObject pointer;
    void Awake() {
        cam = Camera.main;
        hotspot = new Vector2(17, 26);
        Cursor.visible = false;
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null) {
                if (hit.transform.gameObject.layer == 9) {
                    Debug.Log(hit.collider.name);
                    pickedUp = hit.collider.gameObject;
                    pickedUp.GetComponent<DraggingChange>().OnDrag();
                }
                    Debug.Log(hit.collider.name);
            } else {
                Debug.Log("nothing");
            }
        }
        if (Input.GetMouseButton(0)) {
            if (pickedUp != null) {

                pickedUp.transform.position = (cam.ScreenToWorldPoint(Input.mousePosition) + offset);
                pointer.transform.position = (cam.ScreenToWorldPoint(Input.mousePosition) + offset - horseOffset);
            } else {
                pointer.transform.position = (cam.ScreenToWorldPoint(Input.mousePosition) + offset);
            }
            pointer.GetComponent<SpriteRenderer>().sprite = cursor[1];
        }
        if (!Input.GetMouseButton(0)) {
            if (pickedUp != null) {
                pickedUp.GetComponent<DraggingChange>().OnRelease();
            }
            pickedUp = null;
            pointer.transform.position = (cam.ScreenToWorldPoint(Input.mousePosition) + offset);
            pointer.GetComponent<SpriteRenderer>().sprite = cursor[0];
        }
    }

}
