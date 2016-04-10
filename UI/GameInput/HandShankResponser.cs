using UnityEngine;
using System.Collections;

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
        }
    }
}
