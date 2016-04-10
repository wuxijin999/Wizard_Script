using UnityEngine;
using System.Collections;
using UnityEditor;

public class UIToolEditor : Editor {



    [MenuItem("UI/CreatUIRoot")]
    public static void CreatUIRoot() {
        GameObject go = Instantiate(AssetLoadTools.Load_UI("UIRoot"));
        go.name = "UIRoot";
    }
}
