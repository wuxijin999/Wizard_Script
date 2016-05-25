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

    Dictionary<int, FSMState> stateDict;
    bool transformImmediately = false;

    public FSMStateMachine () {
        stateDict = new Dictionary<int, FSMState>();
    }

    public void RegisterState (int _trigger, FSMState _state) {
        stateDict[_trigger] = _state;
    }

    public void SetDefault (int _trigger) {
        currentState = defaultState = stateDict[_trigger];
    }

    public void Begin () {
        currentState.Enter();
    }

    public void SetTrigger (int _trigger) {
        if (!stateDict.ContainsKey(_trigger)) {
            WDebug.Log(string.Format("{0} is not registered!", _trigger));
            return;
        }

        nextState = stateDict[_trigger];
    }

    public void SetTriggerImmediately (int _trigger) {
        transformImmediately = true;
        nextState = stateDict[_trigger];
    }

    public void FSMUpdate () {
        if (currentState == null) {
            return;
        }

        if (transformImmediately) {
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
            transformImmediately = false;
        }

        currentState.Excute();
        if (currentState.Status == FSMSateStatus.Success) {
            currentState.Exit();
            currentState = null;
            if (nextState != null) {
                currentState = nextState;
                currentState.Enter();
                nextState = null;
            }
            else {
                currentState = defaultState;
                currentState.Enter();
            }
        }
    }


}



