//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Monday, October 10, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RepeatedButton))]
public class RepeatedButtonEditor : Editor {

    public override void OnInspectorGUI () {

        RepeatedButton button = target as RepeatedButton;

        serializedObject.Update();
        button.intervalTime = EditorGUILayout.FloatField("Interval", button.intervalTime);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_OnClick"));
        serializedObject.ApplyModifiedProperties();
    }


}



