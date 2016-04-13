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

    protected Actor owner;
    protected CmdType type;
    protected bool mCompleteFlag;
    public bool completeFlag {
        get {
            return mCompleteFlag;
        }
    }

    public ActionCmd(CmdType _type,Actor _owner) {
        type = _type;
        owner = _owner;
    }

    public virtual void Excute() {

    }


}


