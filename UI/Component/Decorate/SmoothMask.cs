using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class SmoothMask : UIBehaviour, ICanvasRaycastFilter {

    private RectTransform rectTransform = null;
    private RectTransform m_Rect {
        get {
            return rectTransform ?? (rectTransform = this.GetComponent<RectTransform>());
        }
    }

    [Range(0, 1)]
    [SerializeField]
    private float m_SmoothHorizontalRate;
    [Range(0, 1)]
    [SerializeField]
    private float m_SmoothVerticalRate;

    [SerializeField]
    private Material m_ImageMat;
    [SerializeField]
    private Material m_TextMat;

    [Tooltip("It's  necessary")]
    [SerializeField]
    private CanvasScaler m_CanvasScale;
    private CanvasScaler canvasScale {
        get {
            return m_CanvasScale ?? (m_CanvasScale = GetComponentInParent<CanvasScaler>());
        }
    }

    Vector2 leftBottom = Vector2.zero;
    Vector2 rightTop = Vector2.zero;

    public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera) {
        return RectTransformUtility.RectangleContainsScreenPoint(m_Rect, sp, eventCamera);
    }

    private void LateUpdate () {
        SmoothMaskUpdate();
    }

    private void SmoothMaskUpdate () {

        leftBottom = m_Rect.TransformPoint(-m_Rect.rect.width * m_Rect.pivot.x, -m_Rect.rect.height * m_Rect.pivot.y, 0) / canvasScale.transform.localScale.x;
        rightTop = m_Rect.TransformPoint(m_Rect.rect.width * (1 - m_Rect.pivot.x), m_Rect.rect.height * (1 - m_Rect.pivot.y), 0) / canvasScale.transform.localScale.x;

        if (m_ImageMat != null) {
            P.SetSmoothMask(m_ImageMat, m_SmoothHorizontalRate, m_SmoothVerticalRate, new Vector4(leftBottom.x, leftBottom.y, rightTop.x, rightTop.y));
        }

        if (m_TextMat != null) {
            P.SetSmoothMask(m_TextMat, m_SmoothHorizontalRate, m_SmoothVerticalRate, new Vector4(leftBottom.x, leftBottom.y, rightTop.x, rightTop.y));
        }
    }

    private static class P {
        public static void SetSmoothMask (Material target, float horizontalRate, float verticalRate, Vector4 _rectWorld) {
            target.SetFloat("_HorizontalRate", horizontalRate);
            target.SetFloat("_VerticalRate", verticalRate);
            target.SetVector("_MaskRect", _rectWorld);
        }
    }

}
