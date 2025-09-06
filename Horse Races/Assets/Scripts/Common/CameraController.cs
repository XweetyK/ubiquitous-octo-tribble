using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    Vector3 init;
    float initZoom;
    Camera cam;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else { Destroy(gameObject); }

        cam = Camera.main;
        init = cam.transform.position;
        initZoom = cam.orthographicSize;
    }

    public void CamZoom(Vector3 pos, float zoom, float speed) {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(pos.x,pos.y,cam.transform.position.z), speed * Time.deltaTime);
        cam.orthographicSize =
            Mathf.Lerp(cam.orthographicSize, zoom, speed * Time.deltaTime);
    }
    public bool CamZoomOut(float speed) {
        bool done=false;
        cam.transform.position = Vector3.Lerp(cam.transform.position, init, speed * Time.deltaTime);
        cam.orthographicSize =
            Mathf.Lerp(cam.orthographicSize, initZoom, speed * Time.deltaTime);
        if (initZoom-cam.orthographicSize<0.1) {
            cam.orthographicSize = initZoom;
            cam.transform.position = init;
            done = true;
        }
        return done;
    }
}
