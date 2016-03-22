//--------------------------------------------------
// 这个是Hud的基类，定义了Hud的最基础属性
//即是，跟随指定的3D目标
//作者：Leonard
//--------------------------------------------------
using UnityEngine;
using System.Collections;

public class HUDbase : MonoBehaviour {

    FacingCamera facingCamera;

    public Transform target {
        get;
        set;
    }
    public Vector3 worldOffset {
        get;
        set;
    }
    public Vector3 screenOffset {
        get;
        set;
    }

    public Camera mCamera {
        get;
        set;
    }

    public void Init(Transform _target, Vector3 _worldOffset, Vector3 _screenOffset, Camera _camera) {

        target = _target;
        worldOffset = _worldOffset;
        screenOffset = _screenOffset;
        mCamera = _camera;
        facingCamera.Init(mCamera);

        //放置在HudRoot下
    }

    protected virtual void Awake() {
        facingCamera = this.gameObject.AddMissComponent<FacingCamera>();
    }

    protected virtual void LateUpdate() {
        Vector3 uiPos = CameraTool.ConvertToUIPosition(mCamera,target.position+worldOffset);
        uiPos += screenOffset;
        this.transform.localPosition = uiPos;
    }
}
