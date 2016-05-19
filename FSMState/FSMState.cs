//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Thursday, May 19, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;



public enum FSMSateStatus {
    Inactive = 0,
    Failure = 1,
    Success = 2,
    Running = 3
}

public class FSMState {

    FSMStateMachine machine;

    public FSMSateStatus Status {
        get; private set;
    }
    public FSMState (FSMStateMachine _machine) {
        machine = _machine;
    }

    public virtual void Enter () {
        Status = FSMSateStatus.Running;
    }

    public virtual void Excute () {

    }

    public virtual void Exit () {

    }

}



