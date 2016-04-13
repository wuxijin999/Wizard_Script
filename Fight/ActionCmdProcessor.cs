using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionCmdProcessor {

    Queue<ActionCmd> commandQueue = new Queue<ActionCmd>();

    ActionCmd actionCmd = null;
    public void PuctCmd(ActionCmd _cmd) {
        commandQueue.Enqueue(_cmd);
    }


    public void Excute() {
        if (commandQueue.Count < 1) {
            return;
        }
        actionCmd = commandQueue.Peek();
        actionCmd.Excute();
        if (actionCmd.completeFlag) {
            commandQueue.Dequeue();
        }
    }


    public void Stop() {

    }






}
