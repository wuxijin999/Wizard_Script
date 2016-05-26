//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Thursday, May 26, 2016
//--------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Button))]
public class MyTest : DecoratorEditor {
    public MyTest () : base("ButtonEditor") { }
    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
        if (GUILayout.Button("Adding this button")) {
            Debug.Log("Adding this button");
        }

    }
}


[CustomEditor(typeof(RectTransform))]
public class MyTest01 : DecoratorEditor {
    public MyTest01 () : base("RectTransformEditor") { }
    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
        if (GUILayout.Button("Adding this button")) {
            Debug.Log("Adding this button");
        }
    }
}

