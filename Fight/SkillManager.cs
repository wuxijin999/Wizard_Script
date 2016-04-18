using UnityEngine;
using System.Collections;

public class SkillManager {

    int skillInstanceId = 1;

    /// <summary>
    /// 请求分配技能实例Id
    /// </summary>
    /// <returns></returns>
    public int AllocateSkillInstanceId() {
        skillInstanceId++;

        return skillInstanceId;
    }



}
