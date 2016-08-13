using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InfiniteItem : InfiniteRect {

    public static int index = 0;
    public static int preIndex = 0;
    public Text txtContent;

    [SerializeField]
    private InfiniteScrollRect m_ScrollRect;
    public InfiniteScrollRect scrollRect {
        get {
            return m_ScrollRect;
        }
        set {
            if (m_ScrollRect != null) {
                m_ScrollRect.CrossTopEvent -= DoFirstToLast;
                m_ScrollRect.CrossBottomEvent -= DoLastToFirst;
            }
            m_ScrollRect = value;

            if (m_ScrollRect != null) {
                m_ScrollRect.CrossTopEvent += DoFirstToLast;
                m_ScrollRect.CrossBottomEvent += DoLastToFirst;
            }
        }
    }

    protected virtual void Start () {
        if (m_ScrollRect != null) {
            m_ScrollRect.CrossTopEvent += DoFirstToLast;
            m_ScrollRect.CrossBottomEvent += DoLastToFirst;
        }
    }

    public virtual void Init () {
        index++;
        txtContent.text = index.ToString();
    }

    public virtual void SetAnchoredPosition (Vector2[] _positions, float[] _times) {
        if (_times[0] < 0.001f) {
            rectTransform.anchoredPosition = _positions[0];
        }

    }

    public virtual void SetAnchoredPosition (Vector2 _positions, float _times) {
        if (_times < 0.001f) {
            rectTransform.anchoredPosition = _positions;
        }

    }

    public virtual void DoFirstToLast (InfiniteItem _item) {
        if (_item != this) {
            return;
        }
        index++;
        preIndex++;
        txtContent.text = index.ToString();
    }

    public virtual void DoLastToFirst (InfiniteItem _item) {
        if (_item != this) {
            return;
        }
        txtContent.text = preIndex.ToString();
        index--;
        preIndex--;
    }

    private void LateUpdate () {

    }


}
