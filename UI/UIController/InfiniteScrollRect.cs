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

    void Start() {
        InitializeChildPosition();
    }
    public void OnBeginDrag(PointerEventData eventData) {
        mRect = this.transform as RectTransform;
        childrenRect = new RectTransform[content.childCount];
        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
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


        //   RefreshPosition();
        lastMousePos = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData) {

    }

    private void Update() {

        //         content.position += new Vector3(0, 1 * Time.deltaTime, 0);
        // 
        //         for (int i = 0; i < childrenRect.Length; i++) {
        //             childrenRect[i] = content.GetChild(i).transform as RectTransform;
        //         }
        // 
        //         if (100 > 0f) {
        //             CrossTopBorderEvent(mRect, childrenRect);
        //         }
        //         else {
        //             CrossBottomBorderEvent(mRect, childrenRect);
        //         }

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

    private void InitializeChildPosition() {
        mRect = this.transform as RectTransform;
        childrenRect = new RectTransform[content.childCount];
        for (int i = 0; i < childrenRect.Length; i++) {
            childrenRect[i] = content.GetChild(i).transform as RectTransform;
        }

        Vector3 offsetMaxWorld = mRect.parent.TransformPoint(mRect.localPosition.x, mRect.offsetMax.y + childrenRect[0].rect.height * 0.5f, 0);
        RectTransform lastRect = childrenRect[0];
        childrenRect[0].position = offsetMaxWorld;

        for (int i = 1; i < childrenRect.Length; i++) {
            childrenRect[i].localPosition = lastRect.localPosition - new Vector3(0, lastRect.rect.height * 0.5f + childrenRect[i].rect.height * 0.5f, 0);
            lastRect = childrenRect[i];
        }

    }
}
