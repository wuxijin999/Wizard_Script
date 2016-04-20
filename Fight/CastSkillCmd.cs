using UnityEngine;
using System.Collections;

namespace Fight {
    public class CastSkillCmd : ActionCmd {

        public CastSkillCmd(CmdType _type, Actor _owner) :
            base(_type, _owner) {

        }

        public override void Begin() {
            base.Begin();
            //让actor进入某种动作状态
        }

        public override void Excute() {
            base.Excute();
            //判断动作状态，是否释放完成，如果是，则标记为complete

        }

        public override void End() {
            base.End();
        }

    }
}

