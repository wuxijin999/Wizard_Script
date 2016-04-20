using UnityEngine;
using System.Collections;

namespace Fight {
    public class Skill {

        int skillId = 0;
        public int SkillId {
            get {
                return skillId;
            }
        }

        int skillInstId = 0;
        public int SkillInstId {
            get {
                return skillInstId;
            }
        }

        public Skill(int _skillId) {
            skillId = _skillId;
            skillInstId = BattleManager.Instance.AllocateSkillInstanceId();
        }


    }

}
