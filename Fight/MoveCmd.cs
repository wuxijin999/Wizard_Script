using UnityEngine;
using System.Collections;

public class MoveCmd : ActionCmd {

    Vector3 targetPosition = Vector3.zero;

    public MoveCmd(CmdType _type, Actor _owner, Vector3 _targetPosition)
        : base(_type, _owner) {

        targetPosition = _targetPosition;
    }

    public override void Excute() {


    }

}
