using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UITest))]
public class WindowTestToolEditor : Editor {

    public override void OnInspectorGUI() {

        UITest tool = (UITest)target;

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(50);
        if (GUILayout.Button("打开测试窗口", GUILayout.ExpandWidth(true))) {
            WindowTestEditorWindow editorWin = new WindowTestEditorWindow();
            editorWin.testTool = tool;
            WindowTestEditorWindow.OpenWindowTest(editorWin);
        }
        GUILayout.Space(50);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        Repaint();

    }


    public static void OpenWindow() {

    }
}
