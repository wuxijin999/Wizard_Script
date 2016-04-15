using UnityEngine;
using System.Collections;

namespace ActionCommand {
    public class CastSkillCmd : ActionCmd {

        public CastSkillCmd(CmdType _type, Actor _owner) :
            base(_type, _owner) {

        }

        public override void Begin() {
            base.Begin();
        }

        public override void Excute() {
            base.Excute();
        }

        public override void End() {
            base.End();
        }

    }
}

