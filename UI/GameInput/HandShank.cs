using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HandShank : SingletonMonobehavior<HandShank>, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler {

    public delegate void HandShankHandler(float _speedRate, Vector2 _vector2);
    public event HandShankHandler DirectionUpdateEvent;

    public Camera mCamera;
    public Image fore;
    public Image backGround;
    public float radius = 1f;

    HandShankState state;

    public Vector3 center {
        get {
            if (backGround != null) {
                return new Vector3(backGround.transform.position.x, backGround.transform.position.y, this.transform.position.z);
            }
            else {
                return this.transform.position;
            }
        }
    }

    protected override void Awake() {
        base.Awake();
        fore.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
    }

    private void OnEnable() {
        fore.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        state = HandShankState.UnActive;
    }

    public void OnPointerDown(PointerEventData eventData) {
        fore.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        fore.transform.position = AmendMousePosition();
        state = HandShankState.Active;
    }

    public void OnBeginDrag(PointerEventData eventData) {

    }

    public void OnDrag(PointerEventData eventData) {
        UpdatePosition();
    }

    public void OnEndDrag(PointerEventData eventData) {
    }

    public void OnPointerUp(PointerEventData eventData) {
        fore.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        state = HandShankState.UnActive;
    }

    private void UpdatePosition() {
        Vector3 mousePosition = AmendMousePosition();
        float distance = Vector3.Distance(center, mousePosition);
        if (distance < 0.001f) {
            return;
        }
        else {
            if (radius > distance) {
                fore.transform.position = mousePosition;
            }
            else {
                float t = radius / distance;
                fore.transform.position = Vector3.Lerp(center, mousePosition, t);
            }
        }
    }

    private void Update() {

        if (state == HandShankState.Active) {
            if (DirectionUpdateEvent != null) {
                DirectionUpdateEvent(CalculateSpeedRate(), CalculateDirection());
            }
        }
    }

    private Vector2 CalculateDirection() {

        if (state == HandShankState.UnActive) {
            return Vector2.zero;
        }
        else {
            Vector2 direction = Vector2.zero;
            direction = Vector3.Normalize(fore.transform.position - center);
            return direction;
        }

    }

    private float CalculateSpeedRate() {
        if (state == HandShankState.UnActive) {
            return 0f;
        }
        else {
            return Mathf.Clamp(Vector3.Distance(fore.transform.position, center) / radius, 0, 1f);
        }
    }

    private Vector3 AmendMousePosition() {
        Vector3 position = mCamera.ScreenToWorldPoint(Input.mousePosition);

        return new Vector3(position.x, position.y, this.transform.position.z);
    }

    public enum HandShankState {
        Active,
        UnActive,
    }
}
