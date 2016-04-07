using UnityEngine;
using System.Collections;

public class ScaleTween : Tween {

    protected override void ApplyNewVector3() {
        base.ApplyNewVector3();
        this.transform.localScale = CalculateVector3();
    }
}
