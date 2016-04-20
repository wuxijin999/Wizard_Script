using UnityEngine;
using System.Collections;

namespace Fight {

    public enum BodyState {
        Idle,
        Walk,
        Run,
        Cast,
        Stuck,                          //硬直
        HitFly,                          //击飞
        HitBack,                       //击退
        HitDown,                     //击倒
        Float,                           //浮空
        Stun,                           //晕眩
        Morph,                       //被变形
        Dead,
    }



}


