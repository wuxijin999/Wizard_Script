using UnityEngine;
using System.Collections;

public class OrganicActor : Actor, IMove {

    #region IMove 成员

    public void MoveTo(Vector3 position, float speed) {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Vector3 position, float speed, float acceleration) {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Transform transform, float speed) {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Transform transform, float speed, float acceleration) {
        throw new System.NotImplementedException();
    }

    public void Follow(Transform transform, Vector3 relativePosition) {
        throw new System.NotImplementedException();
    }

    public void StopFollow() {
        throw new System.NotImplementedException();
    }

    #endregion
}
