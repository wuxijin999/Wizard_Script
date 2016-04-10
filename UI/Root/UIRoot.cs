using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UIRoot : SingletonMonobehavior<UIRoot> {

    protected override void Awake() {
        base.Awake();
        if (EventSystem.current == null) {
            GameObject go = Instantiate(AssetLoadTools.Load_UI("EventSystem"));
            go.name = "EventSystem";
            EventSystem.current = go.GetComponent<EventSystem>();
        }
    }
}
