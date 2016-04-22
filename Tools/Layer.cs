using UnityEngine;
using System.Collections;

public class Layer {


    public static int GroundLayer = LayerMask.NameToLayer("Ground");
    public static int GroundMask = 1 << GroundLayer;

}
