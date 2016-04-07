using UnityEngine;
using System.Collections;

public class RotationTween : Tween {
    protected override void ApplyNewVector3() {
        base.ApplyNewVector3();
        this.transform.localEulerAngles = CalculateVector3();
    }
}
