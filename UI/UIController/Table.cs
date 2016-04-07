using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Table : MonoBehaviour {

    public RectTransform rect;
    public LayoutType Type = LayoutType.Horizontal;
    public RectOffset m_padding;
    public Vector2 m_spacing;

    RectTransform mRect;
    RectTransform[] childrenRect;
    RectTransform rectTransform;
    float delta = 0f;
    bool dirty = false;
    int activedChildCount = 0;

    void Update () {
        int newActivedCount = 0;
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            if (this.transform.GetChild(i).gameObject.activeInHierarchy) {
                newActivedCount++;
            }
        }

        if (newActivedCount != activedChildCount) {
            dirty = true;
            activedChildCount = newActivedCount;
        }

    }


    [ExecuteInEditMode]
    void LateUpdate () {

        if (dirty) {
            ReArrange();
            dirty = false;
        }
    }

    private void ReArrange () {
        if (rect == null) {
            rect = this.transform.GetComponent<RectTransform>();
        }
        switch (Type) {
            case LayoutType.Horizontal:
                delta = m_padding.left;
                for (int i = 0; i < transform.childCount; i++) {
                    rectTransform = transform.GetChild(i) as RectTransform;
                    if (!rectTransform.gameObject.activeInHierarchy) {
                        continue;
                    }
                    rectTransform.anchorMin = new Vector2(0, 0.5f);
                    rectTransform.anchorMax = new Vector2(0, 0.5f);
                    rectTransform.anchoredPosition = new Vector2(delta + rectTransform.rect.width * 0.5f, 0);
                    delta += rectTransform.rect.width + m_spacing.x;
                }
                RecorderChildrenPosition();
                rect.sizeDelta = new Vector2(delta, rect.sizeDelta.y);
                RecoverChildrenPosition();
                break;
            case LayoutType.Vertical:
                delta = -m_padding.top;
                for (int i = 0; i < transform.childCount; i++) {
                    rectTransform = transform.GetChild(i) as RectTransform;
                    if (!rectTransform.gameObject.activeInHierarchy) {
                        continue;
                    }
                    rectTransform.anchorMin = new Vector2(0.5f, 1f);
                    rectTransform.anchorMax = new Vector2(0.5f, 1f);
                    rectTransform.anchoredPosition = new Vector2(0, delta - rectTransform.rect.height * 0.5f);
                    delta -= rectTransform.rect.height + m_spacing.y;
                }
                RecorderChildrenPosition();
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, -delta);
                RecoverChildrenPosition();
                break;
        }

    }

    Vector3[] childrenPosition;
    private void RecorderChildrenPosition () {
        childrenPosition = new Vector3[this.transform.childCount];
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            childrenPosition[i] = this.transform.GetChild(i).position;
        }
    }

    private void RecoverChildrenPosition () {
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            this.transform.GetChild(0).position = childrenPosition[i];
        }
    }


    public enum LayoutType {
        Horizontal,
        Vertical,
    }
}
