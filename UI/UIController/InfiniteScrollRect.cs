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

    public delegate void ScrollHandler();
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
        if (state == MoveState.Springback) {
            return;
        }
        state = MoveState.Normal;
        lastMousePos = mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData) {
        if (state == MoveState.Springback) {
            return;
        }
        mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        offsetY = mousePos.y - lastMousePos.y;
        lastMousePos = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (state == MoveState.Springback) {
            return;
        }
        if (content.localPosition.y > (contentUpBorder + 0.5f)) {
            state = MoveState.Springback;
            content.DOLocalMoveY(contentUpBorder, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
                state = MoveState.Normal;
            });

        }
        else if (content.localPosition.y < (contentDownBorder - 0.5f)) {
            state = MoveState.Springback;
            content.DOLocalMoveY(contentDownBorder, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
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
        contentDownBorder = content.transform.localPosition.y;
        contentUpBorder = contentDownBorder + 16000f;
    }

    private void Update() {
        switch (state) {
            case MoveState.SlowDown:
                velocity -= dampRate * velocity;
                if (Mathf.Abs(velocity) < 1f) {
                    state = MoveState.Normal;
                }
                else {
                    offsetY = velocity * 0.01f * Time.deltaTime;
                }
                break;
        }

    }

    void LateUpdate() {
        if (Mathf.Abs(offsetY) < 0.001f) {
            return;
        }
        content.position += new Vector3(0, offsetY, 0);
        switch (state) {
            case MoveState.Normal:
                break;
            case MoveState.SlowDown:
                float newY = Mathf.Clamp(content.localPosition.y, contentDownBorder, contentUpBorder);
                content.localPosition = new Vector3(content.localPosition.x, newY, content.localPosition.z);
                break;
            case MoveState.Springback:
                break;
        }

        if (offsetY > 0f) {
            if (content.localPosition.y < contentUpBorder) {
                ProcessCrossTopBorder();
            }
        }
        else {
            if (content.localPosition.y > contentDownBorder) {
                ProcessCrossBottomBorder();
            }
        }
        offsetY = 0f;
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
                offsetBottomWorld = infiniteItemArray[i].parent.TransformPoint(offsetBottomLocal
                    - new Vector3(0, infiniteItemArray[i].rect.height * (1f - infiniteItemArray[i].pivot.y), 0));
                offsetBottomLocal = offsetBottomLocal - new Vector3(0, infiniteItemArray[i].rect.height, 0);
                infiniteItemArray[i].rectTransform.position = offsetBottomWorld;
                infiniteItemArray[i].rectTransform.SetAsLastSibling();
                infiniteItemArray[i].DoFirstToLast();
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
                offsetTopWorld = infiniteItemArray[i].parent.TransformPoint(offsetTopLocal
                    + new Vector3(0, infiniteItemArray[i].rect.height * infiniteItemArray[i].pivot.y, 0));
                offsetTopLocal = offsetTopLocal + new Vector3(0, infiniteItemArray[i].rect.height, 0);
                infiniteItemArray[i].rectTransform.position = offsetTopWorld;
                infiniteItemArray[i].rectTransform.SetAsFirstSibling();
                infiniteItemArray[i].DoLastToFirst();
                if (CrossBottomEvent != null) {
                    CrossBottomEvent();
                }
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
