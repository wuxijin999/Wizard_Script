using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InfiniteRect : MonoBehaviour {

    [SerializeField]
    private RectTransform m_Rect;
    public RectTransform rectTransform {
        get {
            if (m_Rect == null) {
                m_Rect = this.transform as RectTransform;
            }
            return m_Rect;
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

    public Vector3 maxmaxPositionWorld {  // x max y max corner world position
        get {
            if (rectTransform.parent != null) {
                return rectTransform.parent.TransformPoint(rectTransform.offsetMax.x, rectTransform.offsetMax.y, rectTransform.anchoredPosition3D.z);
            }
            else {
                return new Vector3(rectTransform.offsetMax.x, rectTransform.offsetMax.y, rectTransform.anchoredPosition3D.z);
            }
        }
    }

    public Vector3 minmaxCornerWorld { // x min y max corner world position
        get {
            if (rectTransform.parent != null) {
                return rectTransform.parent.TransformPoint(rectTransform.offsetMin.x, rectTransform.offsetMax.y, rectTransform.anchoredPosition3D.z);
            }
            else {
                return new Vector3(rectTransform.offsetMin.x, rectTransform.offsetMax.y, rectTransform.anchoredPosition3D.z);
            }
        }
    }

    public Vector3 maxminPositionWorld { // x max y min corner world position
        get {
            if (rectTransform.parent != null) {
                return rectTransform.parent.TransformPoint(rectTransform.offsetMax.x, rectTransform.offsetMin.y, rectTransform.anchoredPosition3D.z);
            }
            else {
                return new Vector3(rectTransform.offsetMax.x, rectTransform.offsetMin.y, rectTransform.anchoredPosition3D.z);
            }
        }
    }

    public Vector3 minminPositionWorld { // x min y min corner world position
        get {
            if (rectTransform.parent != null) {
                return rectTransform.parent.TransformPoint(rectTransform.offsetMin.x, rectTransform.offsetMin.y, rectTransform.anchoredPosition3D.z);
            }
            else {
                return new Vector3(rectTransform.offsetMin.x, rectTransform.offsetMin.y, rectTransform.anchoredPosition3D.z);
            }
        }
    }


    [SerializeField]
    private InfiniteScrollRect m_ScrollRect;
    public InfiniteScrollRect scrollRect {
        get {
            return m_ScrollRect;
        }
        set {
            m_ScrollRect = value;
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
