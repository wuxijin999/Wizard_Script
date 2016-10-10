//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Monday, October 10, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class RepeatedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    #region Fields

    bool m_IsMouseDown = false;
    public bool isMouseDown { get { return m_IsMouseDown; } private set { m_IsMouseDown = value; } }

    float nextTriggerTime = 0f;

    float m_IntervalTime = 0f;
    public float intervalTime {
        get { return m_IntervalTime; }
        set { m_IntervalTime = Mathf.Clamp(value, 0f, float.MaxValue); }
    }

    [SerializeField]
    UnityEvent m_OnClick = new UnityEvent();
    public UnityEvent onClick { get { return m_OnClick; } }

    #endregion

    public RepeatedButton AddListener (UnityAction _action) {
        onClick.AddListener(_action);
        return this;
    }

    public RepeatedButton RemoveListener (UnityAction _action) {
        onClick.RemoveListener(_action);
        return this;
    }

    public RepeatedButton RemoveAllListener () {
        onClick.RemoveAllListeners();
        return this;
    }
    public RepeatedButton SetInterval (float _intervalTime) {
        intervalTime = _intervalTime;
        return this;
    }

    #region Built-In
    void Awake () {

    }

    void Start () {

    }

    void LateUpdate () {
        if (isMouseDown) {
            if (Time.time > nextTriggerTime) {
                nextTriggerTime += intervalTime;
                if (onClick != null) {
                    onClick.Invoke();
                }
            }
        }
    }

    public void OnPointerDown (PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        isMouseDown = true;
        nextTriggerTime = Time.time;
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) {
            return;
        }

        isMouseDown = false;
    }
    #endregion

}



