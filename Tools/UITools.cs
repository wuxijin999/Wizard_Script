using UnityEngine;
using System.Collections;

public class UITools {

    static public void CreatUIRoot () {

        GameObject tempGo = GameObject.Instantiate(AssetLoadTools.Load_UI("UIRoot")) as GameObject;
        GameObject.DontDestroyOnLoad(tempGo);
        tempGo.name = "UIRoot";

    }

    static public void MatchingParent (RectTransform _parent, RectTransform _child) {

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
