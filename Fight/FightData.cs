//------------------------------------------------------------
//           存放Fight相关的数据结构
//
//------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace Fight {

    public class HitData {

        public readonly int hitId;
        public readonly DecisionSphere[] decisionSpheres;

        public readonly FloatData floatData;

        public HitData() {

        }
    }

    public struct DecisionSphere {
        public Vector3 Center;
        public float Radius;
    }

    public struct FloatData {
        public float Height;
        public float RiseTime;
    }

}
