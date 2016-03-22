using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtension {

    public static T AddMissComponent<T>(this GameObject go) where T : Component {

        T component = go.GetComponent<T>();
        if (component == null) {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static T GetComponentByPath<T>(this GameObject go, string path = null) where T : Component {
        Transform t = null;
        if (string.IsNullOrEmpty(path)) {
            t = go.transform;
        }
        else {
            t = go.transform.Find(path);
        }

        T component = t.GetComponent<T>();
        return component;
    }
}
