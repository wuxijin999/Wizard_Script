// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// 
// [CustomEditor(typeof(Transform))]
// public class TransformEditor : Editor {
// 
//     public override void OnInspectorGUI () {
//         base.OnInspectorGUI();
// 
//         Transform t = (Transform)target;
// 
//         if (t.parent != null) {
//             EditorGUILayout.BeginHorizontal();
//             t.localPosition = EditorGUILayout.Vector3Field("LocalPosition", t.localPosition);
//             if (GUILayout.Button("P")) {
//                 t.localPosition = Vector3.zero;
//             }
//             EditorGUILayout.EndHorizontal();
//         }
//         else {
// 
//         }
// 
// 
//     }
// }
