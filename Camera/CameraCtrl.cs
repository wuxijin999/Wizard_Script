using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {

    [System.Serializable]
    public struct CameraData {
        public Vector3 localEuler;
        public Vector3 localPos;
    }
    public Transform followObject;
    public Transform pivot;
    public Transform zoom;

    [SerializeField]
    public CameraData camSetting;

    bool dirty = false;
    float timer = 0f;
    float t = 0f;
    Vector3 posRef;
    Vector3 rotRef;
    Vector3 beginEuler = Vector3.zero;
    Vector3 endEuler = Vector3.zero;
    Vector3 beginPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;

    [SerializeField]
    private float m_Duration;
    public float duration {
        get {
            return Mathf.Clamp(m_Duration, 0.01f, float.MaxValue);
        }
    }

    [SerializeField]
    private Camera m_Camera;
    public Camera mCamera {
        get {
            return m_Camera;
        }
        set {
            m_Camera = value;
        }
    }

    public float ZoomX {
        set {
            camSetting.localPos = camSetting.localPos.SetX(value);
            dirty = true;
        }
    }
    public float ZoomY {
        set {
            camSetting.localPos = camSetting.localPos.SetY(value);
            dirty = true;
        }
    }
    public float ZoomZ {
        set {
            camSetting.localPos =camSetting.localPos.SetZ(value);
            dirty = true;
        }
    }

    public float RotX {
        set {
            camSetting.localEuler = camSetting.localEuler.SetX(value);
            dirty = true;
        }
    }
    public float RotY {
        set {
            camSetting.localEuler = camSetting.localEuler.SetY(value);
            dirty = true;
        }
    }
    public float RotZ {
        set {
            camSetting.localEuler = camSetting.localEuler.SetZ(value);
            dirty = true;
        }
    }

    private void OnEnable () {
        dirty = true;
    }


    private void LateUpdate () {
        if (followObject != null) {
            this.transform.position = followObject.position;
            //this.transform.rotation = FollowObj.rotation;
        }

        if (dirty) {
            timer = duration;
            beginEuler = pivot.localEulerAngles;
            endEuler = camSetting.localEuler;
            beginPos = zoom.localPosition;
            endPos = camSetting.localPos;
            dirty = false;
        }

        if (timer > 0f) {
            timer -= Time.deltaTime;
            t = Mathf.Clamp01((duration - timer) / duration);
            pivot.localEulerAngles = Vector3.Lerp(beginEuler, endEuler, t);
            zoom.localPosition = Vector3.Lerp(beginPos, endPos, t);
        }
    }


    void OnDrawGizmos () {
        if (mCamera != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(mCamera.transform.position, 0.3f);
        }
    }
}
