//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Saturday, May 14, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class RefSkill : RefDataBase {

    public int SkillId {
        get; private set;
    }
    public string SkillName {
        get; private set;
    }
    public float Radius {
        get; private set;
    }
    public int[] PostSkillId {
        get; private set;
    }

    static public RefSkill Get (int _id) {
        RefSkill r = null;

        if (!RefDataManager.Instance.skill.TryGetValue(_id, out r)) {
            WDebug.Log(string.Format("Failed to get skil data by id:<color=yellow>{0}</color> ", _id));
        }

        return r;
    }


}



