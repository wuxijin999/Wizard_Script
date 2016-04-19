using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitDataPool {

    Dictionary<int, HitData> hitDataDict = null;

    public HitDataPool() {
        hitDataDict = new Dictionary<int, HitData>();
    }

    public HitData QueryHitData(int _hitId) {
        HitData hitData = null;
        if (hitDataDict.TryGetValue(_hitId, out hitData)) {
            return hitData;
        }
        else {
            //从本地加载一个
            hitData = new HitData();
            hitDataDict[_hitId] = hitData;
            return hitData;
        }

    }
}
