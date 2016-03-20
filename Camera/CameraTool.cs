using UnityEngine;
using System.Collections;

public class CameraTool {

    /// <summary>
    /// 将世界坐标转换为UI的坐标
    /// </summary>
    /// <param name="_cam"></param>
    /// <param name="_position"></param>
    /// <returns></returns>
    public static Vector3 ConvertToUIPosition(Camera _cam, Vector3 _position) {
        Vector3 UIPos = Vector3.zero;
        Vector3 temp = _cam.WorldToViewportPoint(_position);
        UIPos = CameraManager.UI2DCamera.ViewportToWorldPoint(temp);
        UIPos.z = 0;
        return UIPos;
    }


}
