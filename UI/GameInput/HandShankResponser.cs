using UnityEngine;
using System.Collections;
using Fight;

public class HandShankResponser {

    public float speed {
        get;
        set;
    }

    public IMove iMove {
        get;
        set;
    }

    HandShank handShank;

    public HandShankResponser(HandShank _handShank) {
        handShank = _handShank;
        _handShank.DirectionUpdateEvent += Move;
    }

    public void DisposeDelegate() {
        handShank.DirectionUpdateEvent -= Move;
    }

    void Move(float _speedRate, Vector2 _direction) {
        if (iMove != null) {
            iMove.MoveStep(new Vector3(_direction.x, 0, _direction.y) * speed * _speedRate * Time.deltaTime);
            float dot = Vector3.Dot(Vector3.right, new Vector3(_direction.x, 0, _direction.y));
            float angleY = 0f;
            if (dot > 0) {
                angleY = Vector3.Angle(Vector3.forward, new Vector3(_direction.x, 0, _direction.y));
            }
            else {
                angleY = 360f - Vector3.Angle(Vector3.forward, new Vector3(_direction.x, 0, _direction.y));
            }
            iMove.RotateTo(Quaternion.Euler(0, angleY, 0));
        }
    }
}
