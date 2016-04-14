using UnityEngine;
using System.Collections;

namespace ActionCommand {
    public class MoveCmd : ActionCmd {

        Vector3 targetPosition = Vector3.zero;
        Transform transform = null;
        public MoveCmd(CmdType _type, Actor _owner, Vector3 _targetPosition)
            : base(_type, _owner) {
            transform = _owner.transform;
            targetPosition = _targetPosition;
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Excute() {

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
                mCompleteFlag = true;
            }
        }

        public override void End() {
            base.End();
        }

    }

}

