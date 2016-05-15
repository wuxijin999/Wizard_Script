//------------------------------------------------------------
//           存放Fight相关的数据结构
//
//------------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace Fight {

    public class HitData {

        public int SkillId {
            get; set;
        }

        public int CasterId {
            get; set;
        }

        public readonly int hitId;
        public readonly DecisionSphere[] decisionSpheres;
        public readonly FloatData floatData;

        public HitData ( RefHitData _refHitData) {

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
