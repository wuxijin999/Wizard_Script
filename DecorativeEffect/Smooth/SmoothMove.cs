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
            if (Vector3.Distance(this.transform.localPosition, targetPosition) > 0.001f) {
                Vector3 newPosition = Vector3.SmoothDamp(this.transform.localPosition, targetPosition, ref refPosition, smoothTime);
                this.transform.localPosition = newPosition;
            }
            else {
                if (callBack != null) {
                    callBack();
                    callBack = null;
                }
                Destroy(this);
            }
        }
        else {
            if (Vector3.Distance(this.transform.position, targetPosition) > 0.001f) {
                Vector3 newPosition = Vector3.SmoothDamp(this.transform.position, targetPosition, ref refPosition, smoothTime);
                this.transform.position = newPosition;
            }
            else {
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
