using UnityEngine;
using System.Collections;

public class PositionTween : Tween {

    protected override void ApplyNewVector3() {
        base.ApplyNewVector3();
        this.transform.localPosition = CalculateVector3();
    }
}
