using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum CmdType {
    MoveTo,
    Stand,
    CastSkill,
    Defense,
    Dance,
}


public class ActionCmd {

    protected CmdType type;

    protected bool mCompleteFlag;
    public bool completeFlag {
        get {
            return mCompleteFlag;
        }
    }

    public ActionCmd(CmdType _type) {
        type = _type;
    }

    public virtual void Excute() {

    }


}


