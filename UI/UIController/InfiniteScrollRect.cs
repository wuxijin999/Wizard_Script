//--------------------------------------------
//无限循环控件，基于UGUI
//目前仅支持垂直方向
//---------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class InfiniteScrollRect : InfiniteRect, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public delegate void ScrollHandler(InfiniteItem _item);
    public event ScrollHandler CrossTopEvent;
    public event ScrollHandler CrossBottomEvent;

    public Camera mCamera;
    public RectTransform content;
    public float dampRate = 0f;

    InfiniteItem[] infiniteItemArray = null;
    Vector3 lastMousePos = Vector3.one;
    Vector3 mousePos = Vector3.one;
    float offsetY = 0f;
    float velocity = 0f;

    MoveState state;

    [SerializeField]
    [Range(0, 1)]
    private float normalizeHeight = 0f;
    public float NormalizeHeight {
        get { return normalizeHeight; }
        set { normalizeHeight = value; }
    }

    [SerializeField]
    private float upBorder = 0f;
    protected float UpBorder {
        get {
            return upBorder;
        }
        set {
            upBorder = value;
        }
    }
    [SerializeField]
    private float downBorder = 0f;
    protected float DownBorder {
        get {
            return downBorder;
        }
        set {
            downBorder = value;
        }
    }


    public void OnBeginDrag(PointerEventData eventData) {
        if (state == MoveState.Springback) {
            return;
        }
        state = MoveState.Normal;
        Vector3 p = mCamera.ScreenToWorldPoint(Input.mousePosition);
        lastMousePos = mousePos = this.transform.InverseTransformPoint(p);
    }

    public void OnDrag(PointerEventData eventData) {
        if (state == MoveState.Springback) {
            return;
        }
        Vector3 p = mCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = this.transform.InverseTransformPoint(p);
        float y = mousePos.y - lastMousePos.y;
        normalizeHeight = Mathf.Clamp01(normalizeHeight + y / (upBorder - downBorder));
        lastMousePos = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (state == MoveState.Springback) {
            return;
        }
        if (content.localPosition.y > (upBorder + 0.5f)) {
            state = MoveState.Springback;
            content.DOLocalMoveY(upBorder, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
                state = MoveState.Normal;
            });

        }
        else if (content.localPosition.y < (downBorder - 0.5f)) {
            state = MoveState.Springback;
            content.DOLocalMoveY(downBorder, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
                state = MoveState.Normal;
            });
        }
        else {
            state = MoveState.SlowDown;
            velocity = eventData.delta.y / Time.deltaTime;
        }
    }

    protected void ReArrange() {
        infiniteItemArray = rectTransform.GetComponentsInChildren<InfiniteItem>();

        InfiniteItem lastItem = infiniteItemArray[0];
        for (int i = 1; i < infiniteItemArray.Length; i++) {
            float yRate = lastItem.rectTransform.pivot.y;
            infiniteItemArray[i].rectTransform.localPosition = lastItem.rectTransform.localPosition
                - new Vector3(0, lastItem.rectTransform.rect.height * yRate + infiniteItemArray[i].rectTransform.rect.height * (1 - yRate), 0);
            lastItem = infiniteItemArray[i];
        }
    }

    private void ResetPosition() {
        infiniteItemArray = rectTransform.GetComponentsInChildren<InfiniteItem>();

        Vector3 offsetMaxWorld = minmaxCornerWorld * 0.5f + maxmaxPositionWorld * 0.5f;
        content.position = parent.TransformPoint(center.x, offsetMax.y - content.rect.height * 0.5f, 0);
        InfiniteItem lastItem = infiniteItemArray[0];
        lastItem.rectTransform.position = offsetMaxWorld - new Vector3(0, (lastItem.minmaxCornerWorld - lastItem.minminPositionWorld).y * 0.5f, 0);

        for (int i = 1; i < infiniteItemArray.Length; i++) {
            float yRate = lastItem.rectTransform.pivot.y;
            infiniteItemArray[i].rectTransform.localPosition = lastItem.rectTransform.localPosition
                - new Vector3(0, lastItem.rect.height * yRate + infiniteItemArray[i].rectTransform.rect.height * (1 - yRate), 0);
            lastItem = infiniteItemArray[i];
        }
    }
    void Start() {
        ResetPosition();
        for (int i = 0; i < infiniteItemArray.Length; i++) {
            infiniteItemArray[i].Init();
        }
        InfiniteItem.preIndex = InfiniteItem.index - infiniteItemArray.Length;
        downBorder = content.transform.localPosition.y;
        upBorder = downBorder + 16000f;

    }

    private void Update() {
        switch (state) {
            case MoveState.SlowDown:
                velocity -= dampRate * velocity;
                if (Mathf.Abs(velocity) < 1f) {
                    state = MoveState.Normal;
                }
                else {
                    normalizeHeight = Mathf.Clamp01(normalizeHeight + velocity * Time.deltaTime / (upBorder - downBorder));
                }
                break;
        }

    }

    void LateUpdate() {
        float ty = normalizeHeight * (upBorder - downBorder) + downBorder;
        float d = ty - content.localPosition.y;
        if (Mathf.Abs(d) < 0.1f) {
            return;
        }

        content.localPosition = content.localPosition.SetY(ty);
        switch (state) {
            case MoveState.Normal:
                break;
            case MoveState.SlowDown:
                float newY = Mathf.Clamp(content.localPosition.y, downBorder, upBorder);
                content.localPosition = new Vector3(content.localPosition.x, newY, content.localPosition.z);
                break;
            case MoveState.Springback:
                break;
        }

        if (d > 0f) {
            if (content.localPosition.y < upBorder) {
                ProcessCrossTopBorder();
            }
        }
        else {
            if (content.localPosition.y > downBorder) {
                ProcessCrossBottomBorder();
            }
        }
    }

    /// <summary>
    /// 顶部越界事件
    /// </summary>
    private void ProcessCrossTopBorder() {
        infiniteItemArray = rectTransform.GetComponentsInChildren<InfiniteItem>();

        Vector3 offsetMax = minmaxCornerWorld * 0.5f + maxmaxPositionWorld * 0.5f;
        InfiniteItem lastItem = infiniteItemArray[infiniteItemArray.Length - 1];
        Vector3 offsetBottomLocal = new Vector3(0, lastItem.offsetMin.y, 0);
        Vector3 offsetBottomWorld = lastItem.parent.TransformPoint(offsetBottomLocal);

        Vector3 min = Vector3.zero;
        for (int i = 0; i < infiniteItemArray.Length; i++) {
            min = infiniteItemArray[i].parent.TransformPoint(new Vector3(0, infiniteItemArray[i].offsetMin.y, 0));
            if (min.y > offsetMax.y) {
                if (CrossTopEvent != null) {
                    CrossTopEvent(infiniteItemArray[i]);
                }
                offsetBottomWorld = infiniteItemArray[i].parent.TransformPoint(offsetBottomLocal
                    - new Vector3(0, infiniteItemArray[i].rect.height * (1f - infiniteItemArray[i].pivot.y), 0));
                offsetBottomLocal = offsetBottomLocal - new Vector3(0, infiniteItemArray[i].rect.height, 0);
                infiniteItemArray[i].rectTransform.position = offsetBottomWorld;
                infiniteItemArray[i].rectTransform.SetAsLastSibling();
                infiniteItemArray[i].DoFirstToLast();
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
    private void ProcessCrossBottomBorder() {
        infiniteItemArray = rectTransform.GetComponentsInChildren<InfiniteItem>();

        Vector3 offsetMin = minminPositionWorld * 0.5f + maxminPositionWorld * 0.5f;
        InfiniteItem firstItem = infiniteItemArray[0];
        Vector3 offsetTopLocal = new Vector3(0, firstItem.offsetMax.y, 0);
        Vector3 offsetTopWorld = firstItem.parent.TransformPoint(offsetTopLocal);

        Vector3 max = Vector3.zero;
        for (int i = infiniteItemArray.Length - 1; i >= 0; i--) {
            max = infiniteItemArray[i].parent.TransformPoint(new Vector3(0, infiniteItemArray[i].offsetMax.y, 0));
            if (max.y < offsetMin.y) {
                if (CrossBottomEvent != null) {
                    CrossBottomEvent(infiniteItemArray[i]);
                }
                offsetTopWorld = infiniteItemArray[i].parent.TransformPoint(offsetTopLocal
                    + new Vector3(0, infiniteItemArray[i].rect.height * infiniteItemArray[i].pivot.y, 0));
                offsetTopLocal = offsetTopLocal + new Vector3(0, infiniteItemArray[i].rect.height, 0);
                infiniteItemArray[i].rectTransform.position = offsetTopWorld;
                infiniteItemArray[i].rectTransform.SetAsFirstSibling();
                infiniteItemArray[i].DoLastToFirst();
            }
            else {
                break;
            }
        }

    }

    public enum MoveState {
        Normal,
        SlowDown,
        Springback,
    }

}
