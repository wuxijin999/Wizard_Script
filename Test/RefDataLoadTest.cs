//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Sunday, May 15, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class RefDataLoadTest : MonoBehaviour {

    void Start () {
        RefDataManager.Instance.Init();
        RefSkill hitData = RefSkill.Get(1);

        Debug.Log(
             hitData.SkillId + "\r\n" +
             hitData.SkillName + "\r\n"
            + hitData.Radius + "\r\n"
            + hitData.PostSkillId + "\r\n"
            );
    }

    private void Update () {
        WDebug.Log(Time.frameCount.ToString());
    }

}



