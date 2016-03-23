using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDRoot : SingletonMonobehavior<HUDRoot> {

    private Canvas mCanvas;
    public Canvas MCanvas {
        get {
            return mCanvas;
        }
    }

    protected override void Awake() {
        base.Awake();

        mCanvas = this.transform.GetComponent<Canvas>();
    }

    public void SetRenderCamera(Camera _camera) {
        mCanvas.worldCamera = _camera;
    }

}
