using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InfiniteScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Camera mCamera;
    public RectTransform content;

    Vector3 lastMousePos = Vector3.one;
    Vector3 mousePos = Vector3.one;
    RectTransform mRect;
    RectTransform[] childrenRect = null;

    Action begeinDragCallBack = null;
    Action dragCallBack = null;
    Action endDragCallBack = null;

    public void RegCallBack(Action _beginCallBack, Action _dragCallBack, Action _endCallBack) {
        begeinDragCallBack = _beginCallBack;
        dragCallBack = _dragCallBack;
        endDragCallBack = _endCallBack;
    }


    public void OnBeginDrag(PointerEventData eventData) {
        if (begeinDragCallBack != null) {
            begeinDragCallBack();
        }
        lastMousePos = mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData) {
        mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float y = mousePos.y - lastMousePos.y;
        content.position += new Vector3(0, y, 0);

        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }

        if (y > 0f) {
            CrossTopBorderEvent(mRect, childrenRect);
        }
        else if (y < 0f) {
            CrossBottomBorderEvent(mRect, childrenRect);
        }

        lastMousePos = mousePos;

        if (dragCallBack != null) {
            dragCallBack();
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
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

    private void CrossTopBorderEvent(RectTransform _rect, RectTransform[] _childrenRect) {

        Vector3 offsetMax = _rect.parent.TransformPoint(new Vector3(0, _rect.offsetMax.y, 0));
        RectTransform lastRect = _childrenRect[_childrenRect.Length - 1];
        Vector3 offsetBottomLocal = new Vector3(0, lastRect.offsetMin.y, 0);
        Vector3 offsetBottomWorld = lastRect.parent.TransformPoint(offsetBottomLocal);

        Vector3 min = Vector3.zero;
        for (int i = 0; i < _childrenRect.Length; i++) {
            min = _childrenRect[i].parent.TransformPoint(new Vector3(0, _childrenRect[i].offsetMin.y, 0));
            if (min.y > offsetMax.y) {
                offsetBottomLocal = offsetBottomLocal - new Vector3(0, _childrenRect[i].rect.height * 0.5f, 0);
                offsetBottomWorld = _childrenRect[i].parent.TransformPoint(offsetBottomLocal);
                _childrenRect[i].position = offsetBottomWorld;
                _childrenRect[i].SetAsLastSibling();
            }
            else {
                break;
            }
        }

    }

    private void CrossBottomBorderEvent(RectTransform _rect, RectTransform[] _childrenRect) {

        Vector3 offsetMin = _rect.parent.TransformPoint(new Vector3(0, _rect.offsetMin.y, 0));
        RectTransform firstRect = _childrenRect[0];
        Vector3 offsetTopLocal = new Vector3(0, firstRect.offsetMax.y, 0);
        Vector3 offsetTopWorld = firstRect.parent.TransformPoint(offsetTopLocal);

        Vector3 max = Vector3.zero;
        for (int i = _childrenRect.Length - 1; i >= 0; i--) {
            max = _childrenRect[i].parent.TransformPoint(new Vector3(0, _childrenRect[i].offsetMax.y, 0));
            if (max.y < offsetMin.y) {
                offsetTopLocal = offsetTopLocal + new Vector3(0, _childrenRect[i].rect.height * 0.5f, 0);
                offsetTopWorld = _childrenRect[i].parent.TransformPoint(offsetTopLocal);
                _childrenRect[i].position = offsetTopWorld;
                _childrenRect[i].SetAsFirstSibling();
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
