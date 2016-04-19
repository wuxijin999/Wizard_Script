using UnityEngine;
using System.Collections;


public class HitData {

    public struct DecisionSphere {
        public Vector3 Center;
        public float Radius;
    }

    public readonly int hitId;
    public readonly DecisionSphere[] decisionSpheres;


    public HitData() {

    }

}
