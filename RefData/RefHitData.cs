//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :			 Sunday, May 15, 2016
//--------------------------------------------------------

using System.Collections;
using UnityEngine;
using Fight;

public class RefHitData : RefDataBase {

    public int Id {
        get; private set;
    }

    public AttackMode AttackMode {
        get; private set;
    }

    public int AttackRange {
        get; private set;
    }

    public TargetMode TargetMode {
        get; private set;
    }

    public int TargetNum {
        get; private set;
    }

    public Faction TargetFaction {
        get; private set;
    }

    public DamageType DamageType {
        get; private set;
    }

    public int DamageRate {
        get; private set;
    }

    public Vector3[] DecisionCenter {
        get; private set;
    }

    public float[] DecisionRadius {
        get; private set;
    }

    public int Speed {
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



