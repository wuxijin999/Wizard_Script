using UnityEngine;
using System.Collections;

public class CameraManager {

    /// <summary>
    /// UI Camera 2D
    /// </summary>
    private static Camera ui2DCamera;
    public static Camera UI2DCamera {
        get {
            if (ui2DCamera != null) {
                return ui2DCamera;
            }
            else {
                return null;
            }
        }
    }

    /// <summary>
    /// UI Camera 3D
    /// </summary>
    private static Camera ui3DCamera;
    public static Camera UI3DCamera {
        get {
            if (ui3DCamera != null) {
                return ui3DCamera;
            }
            else {
                return null;
            }
        }
    }

   /// <summary>
   /// 战斗场景主摄像机
   /// </summary>
    private static Camera battleMainCamera;
    public static Camera BattleMainCamera {
        get {
            return battleMainCamera;
        }
    }



  



}
