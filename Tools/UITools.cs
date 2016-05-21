using UnityEngine;
using System.Collections;

public class UITools {

    public static Transform UIRoot = null;
    public static Transform RootWindow = null;

    public static Transform NORMALLAYER = null;
    public static Transform MODALLAYER = null;
    public static Transform TIPSLAYER = null;
    public static Transform SYSTEMLAYER = null;

    private const string NORMAL_LAYER_NAME = "Layer_Normal";
    private const string MODAL_LAYER_NAME = "Layer_Modal";
    private const string TIPS_LAYER_NAME = "Layer_Tips";
    private const string SYSTEM_LAYER_NAME = "Layer_System";

    static public void CreatUIRoot() {

        if (UIRoot != null) {
            Debug.Log("UIRoot 已经创建！");
            return;
        }

        GameObject tempGo = GameObject.Instantiate(AssetLoadTools.Load_UI("UIRoot")) as GameObject;
        GameObject.DontDestroyOnLoad(tempGo);
        tempGo.name = "UIRoot";
        UIRoot = tempGo.transform;
        RootWindow = UIRoot.Find("WindowCanvas");

        GameObject go = null;
        go = new GameObject(NORMAL_LAYER_NAME);
        NORMALLAYER = go.AddComponent<RectTransform>();
        MatchingParent(UITools.RootWindow as RectTransform, NORMALLAYER as RectTransform);

        go = new GameObject(MODAL_LAYER_NAME);
        MODALLAYER = go.AddComponent<RectTransform>();
        MatchingParent(UITools.RootWindow as RectTransform, MODALLAYER as RectTransform);

        go = new GameObject(TIPS_LAYER_NAME);
        TIPSLAYER = go.AddComponent<RectTransform>();
        MatchingParent(UITools.RootWindow as RectTransform, TIPSLAYER as RectTransform);

        go = new GameObject(SYSTEM_LAYER_NAME);
        SYSTEMLAYER = go.AddComponent<RectTransform>();
        MatchingParent(UITools.RootWindow as RectTransform, SYSTEMLAYER as RectTransform);

    }

    static public void MatchingParent(RectTransform _parent, RectTransform _child) {

        if (_child.parent != _parent) {
            _child.SetParent(_parent);
        }
        _child.anchoredPosition3D = Vector3.zero;
        _child.sizeDelta = Vector2.zero;
        _child.anchorMin = Vector2.zero;
        _child.anchorMax = Vector2.one;
        _child.pivot = Vector2.one * 0.5f;
        _child.localRotation = Quaternion.identity;
        _child.localScale = Vector3.one;
    }

}
