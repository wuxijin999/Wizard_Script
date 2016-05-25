//---//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Wednesday, May 25, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class MoveToState : FSMState {

    public Transform transform {
        get; set;
    }

    public Vector3 position {
        get; set;
    }

    public float duration {
        get; set;
    }

    private Vector3 startPosition;
    private float timer;
    public MoveToState () {

    }

    public override void Enter () {
        base.Enter();
        if (transform == null || duration < float.Epsilon) {
            Status = FSMSateStatus.Failure;
            return;
        }

        startPosition = transform.position;
        timer = 0f;
    }

    public override void Excute () {
        base.Excute();
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, position, Mathf.Clamp01(timer / duration));

        if (position == transform.position) {
            Status = FSMSateStatus.Success;
        }
    }

    public override void Exit () {
        base.Exit();
    }

}







