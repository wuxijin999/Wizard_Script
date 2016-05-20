//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Thursday, May 19, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMStateMachine {

    FSMState currentState;
    FSMState nextState;
    FSMState defaultState;

    public FSMStateMachine () {

    }

    public void SetDefaultState (FSMState _defaultState) {
        currentState = defaultState = _defaultState;
        currentState.Enter();
    }

    public void FSMUpdate () {
        if (currentState == null) {
            return;
        }

        currentState.Excute();
        if (currentState.Status == FSMSateStatus.Success) {
            currentState.Exit();
            currentState = null;
            if (nextState != null) {
                currentState = nextState;
                nextState = null;
            }
            else {
                currentState = defaultState;
                currentState.Enter();
            }
        }
    }


}



