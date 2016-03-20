using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
    [System.Serializable]
    public struct CameraData {
        public Vector3 localEuler;
        public Vector3 localPos;
    }
    public float smoothTime = 0.3f;
    public Transform FollowObj;
    public Transform Pivot;
    public Transform Zoom;
    public float Duration;
    public float Distance;

    [SerializeField]
    public CameraData camSetting;

    float timer = 0f;

    Vector3 beginEuler = Vector3.zero;
    Vector3 endEuler = Vector3.zero;
    Vector3 beginPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;

    public float ZoomX {
        set {
            camSetting.localPos = new Vector3(value, camSetting.localPos.y, camSetting.localPos.z);
            resetTarget = true;
        }
    }
    public float ZoomY {
        set {
            camSetting.localPos = new Vector3(camSetting.localPos.x, value, camSetting.localPos.z);
            resetTarget = true;
        }
    }
    public float ZoomZ {
        set {
            camSetting.localPos = new Vector3(camSetting.localPos.x, camSetting.localPos.y, value);
            resetTarget = true;
        }
    }

    public float RotX {
        set {
            camSetting.localEuler = new Vector3(value, camSetting.localEuler.y, camSetting.localEuler.z);
            resetTarget = true;
        }
    }
    public float RotY {
        set {
            camSetting.localEuler = new Vector3(camSetting.localEuler.x, value, camSetting.localEuler.z);
            resetTarget = true;
        }
    }
    public float RotZ {
        set {
            camSetting.localEuler = new Vector3(camSetting.localEuler.x, camSetting.localEuler.y, value);
            resetTarget = true;
        }
    }

    bool resetTarget = false;
    float t = 0f;

    Vector3 posRef;
    Vector3 rotRef;
    private void Update() {

        if (FollowObj != null) {
            Vector3 newPos = Vector3.SmoothDamp(this.transform.position, FollowObj.position, ref posRef, smoothTime);
            Vector3 newRot = Vector3.SmoothDamp(this.transform.eulerAngles, FollowObj.eulerAngles, ref rotRef, smoothTime);
            this.transform.position = newPos;
            this.transform.eulerAngles = newRot;
        }
        else {
            if (resetTarget) {
                timer = Duration;
                beginEuler = Pivot.localEulerAngles;
                endEuler = camSetting.localEuler;
                beginPos = Zoom.localPosition;
                endPos = camSetting.localPos;
                resetTarget = false;
            }

            if (timer > 0f) {
                timer -= Time.deltaTime;
                t = Mathf.Clamp01((Duration - timer) / Duration);
                Pivot.localEulerAngles = Vector3.Lerp(beginEuler, endEuler, t);
                Zoom.localPosition = Vector3.Lerp(beginPos, endPos, t);
            }
        }
    }



}
