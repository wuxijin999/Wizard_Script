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

    public static void SetParentEx(this Transform transform, Transform parent, Vector3 localPosition, Quaternion rotation, Vector3 scale) {

        if (transform != null) {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.localRotation = rotation;
            transform.localScale = scale;
        }
    }


}
