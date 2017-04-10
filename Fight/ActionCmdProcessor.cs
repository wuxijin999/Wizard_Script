using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fight;

public class ActionCmdProcessor {

    Queue<ActionCmd> cmdQueue = new Queue<ActionCmd>();

    ActionCmd actionCmd = null;
    public void PuctCmd (ActionCmd _cmd) {
        cmdQueue.Enqueue(_cmd);
    }


    public void Excute () {
        if (cmdQueue.Count == 0) {
            return;
        }

        actionCmd = cmdQueue.Peek();
        if (actionCmd.completeFlag) {
            actionCmd.End();
            cmdQueue.Dequeue();
            if (cmdQueue.Count > 0) {
                actionCmd = cmdQueue.Peek();
                actionCmd.Begin();
            }
        }
        else {
            actionCmd.Excute();
            if (actionCmd.completeFlag) {
                cmdQueue.Dequeue();
                if (cmdQueue.Count > 0) {
                    actionCmd = cmdQueue.Peek();
                    actionCmd.Begin();
                }
            }
        }

    }


    public void Stop () {

    }






}
