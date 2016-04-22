using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
    [System.Serializable]
    public struct CameraData {
        public Vector3 localEuler;
        public Vector3 localPos;
    }
    public Transform FollowObj;
    public Transform Pivot;
    public Transform Zoom;

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
            camSetting.localPos = new Vector3(value, camSetting.localPos.y, camSetting.localPos.z);
            dirty = true;
        }
    }
    public float ZoomY {
        set {
            camSetting.localPos = new Vector3(camSetting.localPos.x, value, camSetting.localPos.z);
            dirty = true;
        }
    }
    public float ZoomZ {
        set {
            camSetting.localPos = new Vector3(camSetting.localPos.x, camSetting.localPos.y, value);
            dirty = true;
        }
    }

    public float RotX {
        set {
            camSetting.localEuler = new Vector3(value, camSetting.localEuler.y, camSetting.localEuler.z);
            dirty = true;
        }
    }
    public float RotY {
        set {
            camSetting.localEuler = new Vector3(camSetting.localEuler.x, value, camSetting.localEuler.z);
            dirty = true;
        }
    }
    public float RotZ {
        set {
            camSetting.localEuler = new Vector3(camSetting.localEuler.x, camSetting.localEuler.y, value);
            dirty = true;
        }
    }

    private void OnEnable() {
        dirty = true;
    }


    private void LateUpdate() {
        if (FollowObj != null) {
            this.transform.position = FollowObj.position;
            //this.transform.rotation = FollowObj.rotation;
        }

        if (dirty) {
            timer = duration;
            beginEuler = Pivot.localEulerAngles;
            endEuler = camSetting.localEuler;
            beginPos = Zoom.localPosition;
            endPos = camSetting.localPos;
            dirty = false;
        }

        if (timer > 0f) {
            timer -= Time.deltaTime;
            t = Mathf.Clamp01((duration - timer) / duration);
            Pivot.localEulerAngles = Vector3.Lerp(beginEuler, endEuler, t);
            Zoom.localPosition = Vector3.Lerp(beginPos, endPos, t);
        }
    }


    void OnDrawGizmos() {
        if (mCamera!=null) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(mCamera.transform.position,0.3f);
        }
    }
}
