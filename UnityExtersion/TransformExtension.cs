using UnityEngine;
using System.Collections;

public static class TransformExtension {

    public static T GetComponentByPath<T>(this Transform transform, string path = null) where T : Component {
        Transform t = null;
        if (string.IsNullOrEmpty(path)) {
            t = transform;
        }
        else {
            t = transform.Find(path);
        }
        T component = t.GetComponent<T>();
        return component;
    }
}
