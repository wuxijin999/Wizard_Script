//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :			 Sunday, May 15, 2016
//--------------------------------------------------------

using System.Collections;
using UnityEngine;
using Fight;

public class RefHitData : RefDataBase {

    public int id {
        get; private set;
    }

    public AttackMode attackMode {
        get; private set;
    }

    public TargetMode targetMode {
        get; private set;
    }

    public int targetNum {
        get; private set;
    }

    public Faction targetFaction {
        get; private set;
    }

    public DamageType damageType {
        get; private set;
    }

    public int damageRate {
        get; private set;
    }

    public Vector3[] decisionCenter {
        get; private set;
    }

    public float[] decisionRadius {
        get; private set;
    }

    public int speed {
        get; private set;
    }

    static public RefHitData Get (int _id) {
        RefHitData r = null;

        if (!RefDataManager.Instance.hitData.TryGetValue(_id, out r)) {
            WDebug.Log(string.Format("Failed to get RefHitData data by id:<color=yellow>{0}</color> ", _id));
        }

        return r;
    }


}



