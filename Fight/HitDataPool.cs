using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight {

    public class HitDataPool {
        Dictionary<int, Queue<HitData>> hitDataDict = null;

        public HitDataPool () {
            hitDataDict = new Dictionary<int, Queue<HitData>>();
        }

        public HitData GetHitData (int _hitId) {
            HitData hitData = null;
            Queue<HitData> hitDataQueue = null;
            if (hitDataDict.TryGetValue(_hitId, out hitDataQueue)) {
                if (hitDataQueue.Count > 0) {
                    hitData = hitDataQueue.Dequeue();
                }
            }

            return hitData ?? new HitData(RefHitData.Get(_hitId));
        }
    }

}

