//--------------------------------------------
//无限循环控件，基于UGUI
//目前仅支持垂直方向
//---------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InfiniteScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public delegate void ScrollHandler();
    public event ScrollHandler CrossTopEvent;
    public event ScrollHandler CrossBottomEvent;

    public Camera mCamera;
    public RectTransform content;
    public float dampRate = 0f;

    Vector3 lastMousePos = Vector3.one;
    Vector3 mousePos = Vector3.one;
    RectTransform mRect;
    RectTransform[] childrenRect = null;
    float offsetY = 0f;

    bool dirty = false;

    bool inSlowDown = false;
    float velocity = 0f;

    bool inSpringback = false;

    [SerializeField]
    private float contentUpBorder = 0f;
    protected float ContentUpBorder {
        get {
            return contentUpBorder;
        }
        set {
            contentUpBorder = value;
        }
    }
    [SerializeField]
    private float contentDownBorder = 0f;
    protected float ContentDownBorder {
        get {
            return contentDownBorder;
        }
        set {
            contentDownBorder = value;
        }
    }


    public void OnBeginDrag(PointerEventData eventData) {
        inSlowDown = false;
        lastMousePos = mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData) {
        mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        offsetY = mousePos.y - lastMousePos.y;
        lastMousePos = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (content.transform.localPosition.y > (contentUpBorder + 0.5f)) {
            inSpringback = true;
            SmoothMove sMove = content.gameObject.AddMissComponent<SmoothMove>();
            sMove.MoveTo(new Vector3(this.transform.localPosition.x, contentUpBorder, this.transform.localPosition.z), 0.5f, true, () => {
                inSpringback = false;
            });
        }
        else if (content.transform.localPosition.y < (contentDownBorder - 0.5f)) {
            inSpringback = true;
            SmoothMove sMove = content.gameObject.AddMissComponent<SmoothMove>();
            sMove.MoveTo(new Vector3(this.transform.localPosition.x, contentDownBorder, this.transform.localPosition.z), 0.5f, true, () => {
                inSpringback = false;
            });
        }
        else {
            inSlowDown = true;
            velocity = eventData.delta.y / Time.deltaTime;
        }
    }

    protected void ReArrange() {
        mRect = this.transform as RectTransform;
        childrenRect = new RectTransform[content.childCount];
        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }

        RectTransform lastRect = childrenRect[0];
        for (int i = 1; i < childrenRect.Length; i++) {
            float yRate = lastRect.pivot.y;
            childrenRect[i].localPosition = lastRect.localPosition - new Vector3(0, lastRect.rect.height * yRate + childrenRect[i].rect.height * (1 - yRate), 0);
            lastRect = childrenRect[i];
        }
    }

    private void ResetPosition() {

        mRect = this.transform as RectTransform;
        childrenRect = new RectTransform[content.childCount];
        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }

        Vector3 offsetMaxWorld = mRect.parent.TransformPoint(mRect.localPosition.x, mRect.offsetMax.y + childrenRect[0].rect.height * childrenRect[0].pivot.y, 0);
        RectTransform lastRect = childrenRect[0];
        childrenRect[0].position = offsetMaxWorld;

        for (int i = 1; i < childrenRect.Length; i++) {
            float yRate = lastRect.pivot.y;
            childrenRect[i].localPosition = lastRect.localPosition - new Vector3(0, lastRect.rect.height * yRate + childrenRect[i].rect.height * (1 - yRate), 0);
            lastRect = childrenRect[i];
        }
    }
    void Start() {
        ResetPosition();
        contentDownBorder = content.transform.localPosition.y;
        contentUpBorder = contentDownBorder + 1600f;
    }

    private void Update() {
        if (inSlowDown) {
            velocity -= dampRate * velocity;
            if (Mathf.Abs(velocity) < 1f) {
                inSlowDown = false;
            }
            else {
                offsetY = velocity * 0.01f * Time.deltaTime;
            }
        }

    }

    void LateUpdate() {
        if (Mathf.Abs(offsetY) < 0.001f) {
            return;
        }

        content.position += new Vector3(0, offsetY, 0);

        if (inSlowDown) {
            float newY = Mathf.Clamp(content.localPosition.y, contentDownBorder, contentUpBorder);
            content.localPosition = new Vector3(content.localPosition.x, newY, content.localPosition.z);
        }

        if (inSpringback || content.localPosition.y > contentUpBorder || content.localPosition.y < contentDownBorder) {

        }
        else {
            for (int i = 0; i < childrenRect.Length; i++) {
                childrenRect[i] = content.GetChild(i).transform as RectTransform;
            }
            if (offsetY > 0f) {
                ProcessCrossTopBorder(mRect, childrenRect);
            }
            else {
                ProcessCrossBottomBorder(mRect, childrenRect);
            }
        }

        offsetY = 0f;
    }

    /// <summary>
    /// 顶部越界事件
    /// </summary>
    /// <param name="_rect"></param>
    /// <param name="_childrenRect"></param>
    private void ProcessCrossTopBorder(RectTransform _rect, RectTransform[] _childrenRect) {

        Vector3 offsetMax = _rect.parent.TransformPoint(new Vector3(0, _rect.offsetMax.y, 0));
        RectTransform lastRect = _childrenRect[_childrenRect.Length - 1];
        Vector3 offsetBottomLocal = new Vector3(0, lastRect.offsetMin.y, 0);
        Vector3 offsetBottomWorld = lastRect.parent.TransformPoint(offsetBottomLocal);

        Vector3 min = Vector3.zero;
        for (int i = 0; i < _childrenRect.Length; i++) {
            min = _childrenRect[i].parent.TransformPoint(new Vector3(0, _childrenRect[i].offsetMin.y, 0));
            if (min.y > offsetMax.y) {
                offsetBottomWorld = _childrenRect[i].parent.TransformPoint(offsetBottomLocal - new Vector3(0, _childrenRect[i].rect.height * (1f - _childrenRect[i].pivot.y), 0));
                offsetBottomLocal = offsetBottomLocal - new Vector3(0, _childrenRect[i].rect.height, 0);
                _childrenRect[i].position = offsetBottomWorld;
                _childrenRect[i].SetAsLastSibling();
                _childrenRect[i].GetComponent<InfiniteItem>().DoFirstToLast();
                if (CrossTopEvent != null) {
                    CrossTopEvent();
                }
            }
            else {
                break;
            }
        }

    }

    /// <summary>
    /// 底部越界事件
    /// </summary>
    /// <param name="_rect"></param>
    /// <param name="_childrenRect"></param>
    private void ProcessCrossBottomBorder(RectTransform _rect, RectTransform[] _childrenRect) {

        Vector3 offsetMin = _rect.parent.TransformPoint(new Vector3(0, _rect.offsetMin.y, 0));
        RectTransform firstRect = _childrenRect[0];
        Vector3 offsetTopLocal = new Vector3(0, firstRect.offsetMax.y, 0);
        Vector3 offsetTopWorld = firstRect.parent.TransformPoint(offsetTopLocal);

        Vector3 max = Vector3.zero;
        for (int i = _childrenRect.Length - 1; i >= 0; i--) {
            max = _childrenRect[i].parent.TransformPoint(new Vector3(0, _childrenRect[i].offsetMax.y, 0));
            if (max.y < offsetMin.y) {
                offsetTopWorld = _childrenRect[i].parent.TransformPoint(offsetTopLocal + new Vector3(0, _childrenRect[i].rect.height * _childrenRect[i].pivot.y, 0));
                offsetTopLocal = offsetTopLocal + new Vector3(0, _childrenRect[i].rect.height, 0);
                _childrenRect[i].position = offsetTopWorld;
                _childrenRect[i].SetAsFirstSibling();
                _childrenRect[i].GetComponent<InfiniteItem>().DoLastToFirst();
                if (CrossBottomEvent != null) {
                    CrossBottomEvent();
                }
            }
            else {
                break;
            }
        }

    }




}
