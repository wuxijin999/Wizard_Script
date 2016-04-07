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
    float springBackStartY = 0f;
    float springBackDuration = 0.2f;
    float springBackVelocityRef = 0f;

    Action begeinDragCallBack = null;
    Action endDragCallBack = null;

    private float contentUpBorder = 0f;
    protected float ContentUpBorder {
        get {
            return contentUpBorder;
        }
        set {
            contentUpBorder = value;
        }
    }

    private float contentDownBorder = 0f;
    protected float ContentDownBorder {
        get {
            return contentDownBorder;
        }
        set {
            contentDownBorder = value;
        }
    }

    public void RegCallBack(Action _beginCallBack, Action _endCallBack) {
        begeinDragCallBack = _beginCallBack;
        endDragCallBack = _endCallBack;
    }

    public void ReArrange() {

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

    public void OnBeginDrag(PointerEventData eventData) {
        inSlowDown = false;
        if (begeinDragCallBack != null) {
            begeinDragCallBack();
        }
        lastMousePos = mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData) {
        mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        offsetY = mousePos.y - lastMousePos.y;
        lastMousePos = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData) {

        inSlowDown = true;
        velocity = eventData.delta.y / Time.deltaTime;

        if (content.transform.position.y > contentUpBorder || content.transform.position.y < contentDownBorder) {
            inSpringback = true;
        }

        if (endDragCallBack != null) {
            endDragCallBack();
        }
    }

    protected void ResetPosition() {
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

    void Start() {
        ResetPosition();
        ReArrange();
    }

    private void Update() {
        if (inSlowDown) {
            velocity -= dampRate * velocity;
            offsetY = velocity * 0.01f * Time.deltaTime;
            if (Mathf.Abs(velocity) < 1f) {
                inSlowDown = false;
            }
        }

        if (inSpringback) {
            float recorderY = 0f;
            float newY = 0f;
            if (springBackStartY > contentUpBorder) {
                newY = Mathf.SmoothDamp(springBackStartY, contentUpBorder, ref springBackVelocityRef, springBackDuration);
            }
            else {
                newY = Mathf.SmoothDamp(springBackStartY, contentUpBorder, ref springBackVelocityRef, springBackDuration);
            }

            offsetY = recorderY - newY;

            if (offsetY < 0.0001f) {
                inSpringback = false;
            }
        }


    }

    void LateUpdate() {
        if (Mathf.Abs(offsetY) < 0.001f) {
            return;
        }

        content.position += new Vector3(0, offsetY, 0);

        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }

        if (offsetY > 0f) {
            CrossTopBorderEvent(mRect, childrenRect);
        }
        else {
            CrossBottomBorderEvent(mRect, childrenRect);
        }

        offsetY = 0f;
    }

    /// <summary>
    /// 顶部越界事件
    /// </summary>
    /// <param name="_rect"></param>
    /// <param name="_childrenRect"></param>
    private void CrossTopBorderEvent(RectTransform _rect, RectTransform[] _childrenRect) {

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
    private void CrossBottomBorderEvent(RectTransform _rect, RectTransform[] _childrenRect) {

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
            }
            else {
                break;
            }
        }

    }




}
