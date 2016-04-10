using UnityEngine;
using System.Collections;

public class FacingCamera : MonoBehaviour {

    public Camera mCamera;

    public void Init(Camera _camera) {
        mCamera = _camera;
    }

    private void LateUpdate() {
        this.transform.rotation = mCamera.transform.rotation;
    }
}
