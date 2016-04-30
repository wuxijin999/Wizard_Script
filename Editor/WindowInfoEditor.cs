using UnityEditor;
using UnityEngine;
using UI;

[CustomEditor(typeof(WindowInfo))]
public class WindowInfoEditor : Editor {

    public override void OnInspectorGUI() {

        WindowInfo info = (WindowInfo)target;
        info.Type = (WindowType)EditorGUILayout.EnumPopup("窗口类型", (WindowType)info.Type);
        EditorGUILayout.Space();
        info.AnimType = (WinAnimType)EditorGUILayout.EnumPopup("动画类型", (WinAnimType)info.AnimType);

        switch (info.AnimType) {
            case WinAnimType.OffSet:
                EditorGUI.indentLevel++;
                info.OffsetStyle = (WinOffsetAnimStyle)EditorGUILayout.EnumPopup("位移动画风格", (WinOffsetAnimStyle)info.OffsetStyle);
                EditorGUI.indentLevel--;
                break;
        }
        EditorGUILayout.Space();
        info.ClickEmptyToClose = EditorGUILayout.Toggle("点击空白关闭", info.ClickEmptyToClose);
        if (info.ClickEmptyToClose) {
            info.NeedMask = true;
        }
        EditorGUILayout.Space();
        info.NeedMask = EditorGUILayout.Toggle("添加遮罩", info.NeedMask);
        if (info.NeedMask) {
            EditorGUI.indentLevel++;
            info.MaskAlpha = EditorGUILayout.IntSlider("透明度", info.MaskAlpha, 1, 255);
            EditorGUI.indentLevel--;
        }

        Repaint();

    }
}
