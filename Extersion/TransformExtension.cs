using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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


    public static T[] GetComponentsInChildren<T>(this Transform transform, bool includeInactive, bool includeSelf) where T : Component {

        if (includeSelf) {
            return transform.GetComponentsInChildren<T>(includeInactive);
        }
        else {
            int childCount = transform.childCount;
            List<T> list = new List<T>();
            T t = null;
            for (int i = 0; i < childCount; i++) {
                t = transform.GetComponent<T>();
                if (t != null) {
                    list.Add(t);
                }
            }
            return list.ToArray();
        }

    }

}
