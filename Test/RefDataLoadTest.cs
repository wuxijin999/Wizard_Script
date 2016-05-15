//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Sunday, May 15, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class RefDataLoadTest : MonoBehaviour {

    void Start () {
        RefDataManager.Instance.Init();
        RefHitData hitData = RefHitData.Get(1);

        Debug.Log(
             hitData.Id + "\r\n" +
             hitData.AttackMode + "\r\n"
            + hitData.TargetMode + "\r\n"
            + hitData.TargetNum + "\r\n"
            + hitData.TargetFaction + "\r\n"
            + hitData.DamageType + "\r\n"
            + hitData.DamageRate + "\r\n"
            + hitData.DecisionCenter[0] + "\r\n"
            + hitData.DecisionRadius[0] + "\r\n"
            );
    }

}



