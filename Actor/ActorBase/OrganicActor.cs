using UnityEngine;
using System.Collections;
using System;

public class OrganicActor : Actor, IMove {
    public void Follow(Transform transform, Vector3 relativePosition) {
        throw new NotImplementedException();
    }

    public void MoveStep(Vector3 deltaVector3) {
        throw new NotImplementedException();
    }

    public void MoveTo(Vector3 position) {
        throw new NotImplementedException();
    }

    public void MoveTo(Transform transform, float speed) {
        throw new NotImplementedException();
    }

    public void MoveTo(Vector3 position, float speed) {
        throw new NotImplementedException();
    }

    public void MoveTo(Transform transform, float speed, float acceleration) {
        throw new NotImplementedException();
    }

    public void MoveTo(Vector3 position, float speed, float acceleration) {
        throw new NotImplementedException();
    }

    public void StopFollow() {
        throw new NotImplementedException();
    }
}
