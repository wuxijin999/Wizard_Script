using UnityEngine;
using System.Collections;

public class FacingCamera : MonoBehaviour {

    public Camera camera;

    public void Init(Camera _camera) {
        camera = _camera;
    }

    private void LateUpdate() {
        this.transform.rotation = camera.transform.rotation;
    }
}
