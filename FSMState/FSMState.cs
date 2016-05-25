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

    public FSMSateStatus Status {
        get; protected set;
    }
    public FSMState () {

    }

    public virtual void Enter () {
        Status = FSMSateStatus.Running;
    }

    public virtual void Excute () {

    }

    public virtual void Exit () {

    }

}



