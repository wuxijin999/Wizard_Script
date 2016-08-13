using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InfiniteRect : MonoBehaviour {

    [SerializeField]
    private RectTransform m_Rect;
    public RectTransform rectTransform {
        get {
            return m_Rect ?? (m_Rect = this.transform as RectTransform);
        }
    }

    public Transform parent {
        get {
            return rectTransform.parent;
        }
    }

    public Vector2 pivot {
        get {
            return rectTransform.pivot;
        }
        set {
            rectTransform.pivot = value;
        }
    }

    public Rect rect {
        get {
            return rectTransform.rect;
        }
    }

    public Vector2 offsetMin {
        get {
            return rectTransform.offsetMin;
        }
    }

    public Vector2 offsetMax {
        get {
            return rectTransform.offsetMax;
        }
    }

    public Vector2 sizeDelta {
        get {
            return rectTransform.sizeDelta;
        }
        set {
            rectTransform.sizeDelta = value;
        }
    }

    public Vector3 maxmaxCornerWorld {  // x max y max corner world position
        get {
            return rectTransform.TransformPoint(rect.width * (1 - pivot.x), rect.height * (1 - pivot.y), rectTransform.anchoredPosition3D.z);
        }
    }

    public Vector3 minmaxCornerWorld { // x min y max corner world position
        get {
            return rectTransform.TransformPoint(-rect.width * pivot.x, rect.height * (1 - pivot.y), rectTransform.anchoredPosition3D.z);
        }
    }

    public Vector3 maxminCornerWorld { // x max y min corner world position
        get {
            return rectTransform.TransformPoint(rect.width * (1 - pivot.x), -rect.height * pivot.y, rectTransform.anchoredPosition3D.z);
        }
    }

    public Vector3 minminCornerWorld { // x min y min corner world position
        get {
            return rectTransform.TransformPoint(-rect.width * pivot.x, -rect.height * pivot.y, rectTransform.anchoredPosition3D.z);
        }
    }

    public Vector2 center {
        get {
            return rectTransform.offsetMax - new Vector2(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f);
        }
    }

    public Vector3 centerWorld {
        get {
            if (rectTransform.parent != null) {
                return rectTransform.parent.TransformPoint(center.x, center.y, rectTransform.localPosition.z);
            }
            else {
                return new Vector3(center.x, center.y, rectTransform.position.z);
            }
        }
    }
}
