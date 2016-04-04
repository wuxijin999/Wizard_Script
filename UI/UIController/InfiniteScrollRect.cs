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
    bool inSlowDown = false;
    float initialVelocity = 0f;

    Action begeinDragCallBack = null;
    Action endDragCallBack = null;

    public void RegCallBack(Action _beginCallBack, Action _endCallBack) {
        begeinDragCallBack = _beginCallBack;
        endDragCallBack = _endCallBack;
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
        initialVelocity = eventData.delta.y / Time.deltaTime;
        if (endDragCallBack != null) {
            endDragCallBack();
        }
    }

    void Start() {
        mRect = this.transform as RectTransform;
        childrenRect = new RectTransform[content.childCount];
        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }
        InitializeChildPosition(mRect, childrenRect);
    }

    private void Update() {
        if (inSlowDown) {
            initialVelocity -= dampRate * initialVelocity;
            offsetY = initialVelocity * 0.01f * Time.deltaTime;
            if (Mathf.Abs(initialVelocity) < 1f) {
                inSlowDown = false;
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
                offsetBottomWorld = _childrenRect[i].parent.TransformPoint(offsetBottomLocal - new Vector3(0, _childrenRect[i].rect.height * 0.5f, 0));
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
                offsetTopWorld = _childrenRect[i].parent.TransformPoint(offsetTopLocal + new Vector3(0, _childrenRect[i].rect.height * 0.5f, 0));
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

    private void InitializeChildPosition(RectTransform _mRect, RectTransform[] _childrenRect) {

        Vector3 offsetMaxWorld = _mRect.parent.TransformPoint(_mRect.localPosition.x, _mRect.offsetMax.y + _childrenRect[0].rect.height * 0.5f, 0);
        RectTransform lastRect = _childrenRect[0];
        _childrenRect[0].position = offsetMaxWorld;

        for (int i = 1; i < _childrenRect.Length; i++) {
            _childrenRect[i].localPosition = lastRect.localPosition - new Vector3(0, lastRect.rect.height * 0.5f + _childrenRect[i].rect.height * 0.5f, 0);
            lastRect = _childrenRect[i];
        }

    }
}
