using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {

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

    public void Init() {
    
    }

    protected virtual void LateUpdate() {
    
    }




}
