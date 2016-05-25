//---//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Friday, May 20, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class WaitTimeState : FSMState {

    public float Duration {
        get; set;
    }

    private float endTime;
    public WaitTimeState () {

    }

    public override void Enter () {
        base.Enter();
        if (Duration < 0) {
            Status = FSMSateStatus.Failure;
        }
        else {
            Status = FSMSateStatus.Running;
            endTime = Time.time + Duration;
        }

    }

    float triggerTimer = 1f;
    public override void Excute () {
        base.Excute();
        if (Time.time < endTime) {
            triggerTimer += Time.deltaTime;
            if (triggerTimer > 1f) {
                triggerTimer = 0;
                Debug.Log(Time.time);
            }
        }
        else {
            Status = FSMSateStatus.Success;
        }
    }

    public override void Exit () {
        base.Exit();
    }

}







