using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class UIToolEditor : Editor {



    [MenuItem("UI/CreatUIRoot")]
    public static void CreateUIRoot() {
        GameObject go = Instantiate(AssetLoad.LoadUI("UIRoot"));
        go.name = "UIRoot";
    }


    [MenuItem("UI/ButtonEmpty")]
    public static void CreateEmptyButton() {
        GameObject go = new GameObject();
        go.AddMissComponent<RectTransform>();
        UIRaycastTarget r = go.AddMissComponent<UIRaycastTarget>();
        Button b = go.AddMissComponent<Button>();
        b.transition = Selectable.Transition.None;
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas != null) {
            go.transform.SetParentEx(canvas.transform, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        go.name = "ButtonEmpty";
    }
}
