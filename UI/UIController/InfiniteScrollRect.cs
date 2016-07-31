﻿//--------------------------------------------
//无限循环控件，基于UGUI
//目前仅支持垂直方向
//---------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class InfiniteScrollRect : InfiniteRect, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public delegate void ScrollHandler (InfiniteItem _item);
    public event ScrollHandler CrossTopEvent;
    public event ScrollHandler CrossBottomEvent;

    [SerializeField]
    private RectTransform m_Content;
    [SerializeField]
    private Vector2 m_CellSize = Vector2.zero;
    [SerializeField]
    private float m_Elasticity = 0.1f;
    [SerializeField]
    private float m_DecelerationRate = 0.135f;
    [SerializeField]
    [Range(0, 1)]
    private float m_NormalizeHeight = 0f;
    [SerializeField]
    private float upBorder = 0f;
    [SerializeField]
    private float downBorder = 0f;

    Vector2 startMousePosition = Vector2.zero;
    Vector2 startContentPosition = Vector2.zero;
    Vector2 prevPosition = Vector2.zero;
    float velocity = 0f;
    bool dragging = false;
    List<InfiniteItem> infiniteItemList = null;

    public void OnBeginDrag (PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        startMousePosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out startMousePosition);
        prevPosition = startContentPosition = m_Content.anchoredPosition;

        dragging = true;
    }

    public void OnDrag (PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 localMouse;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localMouse))
            return;

        var pointerDelta = localMouse - startMousePosition;
        Vector2 position = startContentPosition + pointerDelta;
        SetContentAnchoredPosition(position);
    }

    public void OnEndDrag (PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        dragging = false;
    }


    void Start () {
        ResetPosition();
        for (int i = 0; i < infiniteItemList.Count; i++) {
            infiniteItemList[i].Init();
        }
        InfiniteItem.preIndex = InfiniteItem.index - infiniteItemList.Count;

    }

    void LateUpdate () {
        if (m_Content == null) {
            return;
        }

        float deltaTime = Time.unscaledDeltaTime;
        float offset = CalculateOffset();

        if (!dragging && (velocity != 0f || offset != 0f)) {
            Vector2 position = m_Content.anchoredPosition;
            if (offset != 0) {
                float speed = velocity;
                position[1] = Mathf.SmoothDamp(m_Content.anchoredPosition[1], m_Content.anchoredPosition[1] - offset, ref speed, m_Elasticity, Mathf.Infinity, deltaTime);
                velocity = speed;
            }
            else {
                velocity *= Mathf.Pow(m_DecelerationRate, deltaTime);
                if (Mathf.Abs(velocity) < 1) {
                    velocity = 0;
                }
                position[1] += velocity * deltaTime;
            }

            SetContentAnchoredPosition(position);
        }

        if (dragging && m_Content.anchoredPosition != prevPosition) {
            float newVelocity = (m_Content.anchoredPosition[1] - prevPosition[1]) / deltaTime;
            velocity = Mathf.Lerp(velocity, newVelocity, deltaTime * 10);
            prevPosition = m_Content.anchoredPosition;
        }
    }

    private void ResetPosition () {
        if (infiniteItemList == null) {
            infiniteItemList = new List<InfiniteItem>();
            InfiniteItem item = null;
            int childCount = m_Content.childCount;
            for (int i = 0; i < childCount; i++) {
                item = m_Content.GetChild(i).GetComponent<InfiniteItem>();
                if (item != null) {
                    infiniteItemList.Add(item);
                }
            }
        }

        Vector3 offsetMaxWorld = minmaxCornerWorld * 0.5f + maxmaxCornerWorld * 0.5f;
        m_Content.position = parent.TransformPoint(center.x, offsetMax.y - m_Content.rect.height * 0.5f, 0);
        InfiniteItem lastItem = infiniteItemList[0];
        lastItem.rectTransform.position = offsetMaxWorld - new Vector3(0, (lastItem.minmaxCornerWorld - lastItem.minminCornerWorld).y * 0.5f, 0);

        for (int i = 1; i < infiniteItemList.Count; i++) {
            infiniteItemList[i].rectTransform.anchoredPosition = lastItem.rectTransform.anchoredPosition.AddY(-m_CellSize.y);
            lastItem = infiniteItemList[i];
        }

        downBorder = m_Content.transform.localPosition.y;
        upBorder = downBorder + 3200f;
    }

    private void SetContentAnchoredPosition (Vector2 _position) {

        if (_position != m_Content.anchoredPosition) {
            _position.x = m_Content.anchoredPosition.x;
            float d = _position.y - m_Content.anchoredPosition.y;
            m_Content.anchoredPosition = _position;
            if (d > 0f) {
                if (m_Content.anchoredPosition.y < upBorder) {
                    ProcessCrossTopBorder();
                }
            }
            else {
                if (m_Content.anchoredPosition.y > downBorder) {
                    ProcessCrossBottomBorder();
                }
            }
        }
    }

    private float CalculateOffset () {
        float offset = 0f;

        if (m_Content.anchoredPosition.y > upBorder) {
            offset = m_Content.anchoredPosition.y - upBorder;
        }
        else if (m_Content.anchoredPosition.y < downBorder) {
            offset = m_Content.anchoredPosition.y - downBorder;
        }

        return offset;
    }

    /// <summary>
    /// 顶部越界事件
    /// </summary>
    private void ProcessCrossTopBorder () {

        Vector3 offsetMax = minmaxCornerWorld * 0.5f + maxmaxCornerWorld * 0.5f;
        InfiniteItem lastItem = infiniteItemList[infiniteItemList.Count - 1];
        Vector3 offsetBottomLocal = new Vector3(0, lastItem.offsetMin.y, 0);
        Vector3 offsetBottomWorld = lastItem.parent.TransformPoint(offsetBottomLocal);

        Vector3 min = Vector3.zero;
        InfiniteItem item = null;
        for (int i = 0; i < infiniteItemList.Count; i++) {
            item = infiniteItemList[i];
            min = item.parent.TransformPoint(new Vector3(0, item.offsetMin.y, 0));
            if (min.y > offsetMax.y) {
                if (CrossTopEvent != null) {
                    CrossTopEvent(item);
                }
                offsetBottomWorld = item.parent.TransformPoint(offsetBottomLocal - new Vector3(0, item.rect.height * (1f - item.pivot.y), 0));
                offsetBottomLocal = offsetBottomLocal - new Vector3(0, item.rect.height, 0);
                item.rectTransform.position = offsetBottomWorld;

                infiniteItemList.Remove(item);
                infiniteItemList.Add(item);
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
    private void ProcessCrossBottomBorder () {

        Vector3 offsetMin = minminCornerWorld * 0.5f + maxminCornerWorld * 0.5f;
        InfiniteItem firstItem = infiniteItemList[0];
        Vector3 offsetTopLocal = new Vector3(0, firstItem.offsetMax.y, 0);
        Vector3 offsetTopWorld = firstItem.parent.TransformPoint(offsetTopLocal);

        Vector3 max = Vector3.zero;
        InfiniteItem item = null;
        for (int i = infiniteItemList.Count - 1; i >= 0; i--) {
            item = infiniteItemList[i];
            max = item.parent.TransformPoint(new Vector3(0, item.offsetMax.y, 0));
            if (max.y < offsetMin.y) {
                if (CrossBottomEvent != null) {
                    CrossBottomEvent(item);
                }
                offsetTopWorld = item.parent.TransformPoint(offsetTopLocal + new Vector3(0, item.rect.height * item.pivot.y, 0));
                offsetTopLocal = offsetTopLocal + new Vector3(0, item.rect.height, 0);
                item.rectTransform.position = offsetTopWorld;

                infiniteItemList.Remove(item);
                infiniteItemList.Insert(0, item);
            }
            else {
                break;
            }
        }

    }
}
