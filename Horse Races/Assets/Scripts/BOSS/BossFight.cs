using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {
    public static BossFight Instance;
    enum Clash { ZoomIn,ZoomOut,None};

    [SerializeField] float camSpeed;
    [SerializeField] float camDist;
    [SerializeField] Vector3 offset;
    Vector3 camPos;
    HorseBehavior Boss;
    Clash clashing = Clash.None;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else { Destroy(gameObject); }
    }

    public void StartClash(HorseBehavior boss, GameObject target) {
        Boss = boss;
        camPos = (Vector3.Lerp(boss.transform.position, target.transform.position, 0.5f) - offset);
        clashing = Clash.ZoomIn;
    }

    private void Update() {
        switch (clashing) {
            case Clash.ZoomIn:
                CameraController.Instance.CamZoom(camPos, camDist, camSpeed);
                if (Input.GetKeyDown(KeyCode.Z)) {
                    clashing = Clash.ZoomOut;
                    Boss.ResumeClash();
                }
                break;
            case Clash.ZoomOut:
                if (CameraController.Instance.CamZoomOut(camSpeed)) {
                    clashing = Clash.None;
                }
                break;
            case Clash.None:
                break;
        }
    }
}
