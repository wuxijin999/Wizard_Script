using UnityEngine;
using System.Collections;
using System;

public class SmoothMove : MonoBehaviour {

    Vector3 targetPosition = Vector3.zero;
    float smoothTime = 0f;
    bool isLocal = false;
    Action callBack = null;

    Vector3 refPosition = Vector3.zero;
    bool startSmooth = false;

    public static void MoveTo(GameObject _gameObject, Vector3 _targetPosition, float _smoothTime, bool _isLocal, Action _callBack) {
        SmoothMove smove = _gameObject.AddMissComponent<SmoothMove>();
        smove.MoveTo(_targetPosition, _smoothTime, _isLocal, _callBack);
    }

    public void MoveTo(Vector3 _targetPosition, float _smoothTime, bool _isLocal, Action _callBack) {

        targetPosition = _targetPosition;
        smoothTime = _smoothTime;
        isLocal = _isLocal;
        callBack = _callBack;

        startSmooth = true;
    }

    private void LateUpdate() {
        if (!startSmooth) {
            return;
        }

        if (isLocal) {
            if (Vector3.Distance(this.transform.localPosition, targetPosition) > 1f) {
                Vector3 newPosition = Vector3.SmoothDamp(this.transform.localPosition, targetPosition, ref refPosition, smoothTime);
                this.transform.localPosition = newPosition;
            }
            else {
                this.transform.localPosition = targetPosition;
                if (callBack != null) {
                    callBack();
                    callBack = null;
                }
                Destroy(this);
            }
        }
        else {
            if (Vector3.Distance(this.transform.position, targetPosition) > 0.01f) {
                Vector3 newPosition = Vector3.SmoothDamp(this.transform.position, targetPosition, ref refPosition, smoothTime);
                this.transform.position = newPosition;
            }
            else {
                this.transform.position = targetPosition;
                if (callBack != null) {
                    callBack();
                    callBack = null;
                }
                Destroy(this);
            }
        }

    }


    void OnDisable() {
        if (callBack != null) {
            callBack();
            callBack = null;
        }
        startSmooth = false;
    }
}
