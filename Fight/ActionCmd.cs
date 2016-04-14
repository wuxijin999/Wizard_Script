using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionCommand {
    public enum CmdType {
        None,
        MoveTo,
        Stand,
        CastSkill,
        Defense,
        Dance,
    }

    public enum TaskStatus {
        Wait,
        Doing,
        End
    }

    public class ActionCmd {

        protected TaskStatus mStatus;
        public TaskStatus status {
            get {
                return mStatus;
            }
        }

        protected Actor owner;
        protected CmdType type;
        protected bool mCompleteFlag;
        public bool completeFlag {
            get {
                return mCompleteFlag;
            }
        }

        public ActionCmd(CmdType _type, Actor _owner) {
            type = _type;
            owner = _owner;
            mStatus = TaskStatus.Wait;
        }

        public virtual void Begin() {
            mStatus = TaskStatus.Doing;
        }

        public virtual void Excute() {

        }

        public virtual void End() {
            mStatus = TaskStatus.End;
            type = CmdType.None;
            owner = null;
        }

    }
}



