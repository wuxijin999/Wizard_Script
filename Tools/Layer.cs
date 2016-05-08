using UnityEngine;
using System.Collections;

public class Layer {


    public static int GroundLayer = LayerMask.NameToLayer("Ground");
    public static int GroundMask = 1 << GroundLayer;

    public static int UILayer = LayerMask.NameToLayer("UI");
    public static int UIMask = 1 << UILayer;

    public static int HUDLayer = LayerMask.NameToLayer("HUD");
    public static int HUDMask = 1 << HUDLayer;

}
