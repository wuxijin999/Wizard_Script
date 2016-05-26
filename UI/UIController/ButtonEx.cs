using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonEx : Button, ICanvasRaycastFilter {

    public float ForbidTime = 1f;
    public ResponseType Response;

    float interactableTime = 0f;

    public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera) {
        if (Response == ResponseType.Off || Response == ResponseType.Gray) {
            return Time.time > interactableTime;
        }
        else {
            return true;
        }
    }

    public override void OnPointerClick (PointerEventData eventData) {
        if (Response == ResponseType.Tip) {
            Debug.Log("Disable！");
            return;
        }
        base.OnPointerClick(eventData);
        interactableTime = Time.time + ForbidTime;
    }

    protected override void Start () {
        base.Start();
        interactableTime = Time.time;
    }

    public enum ResponseType {
        None,
        Off,
        Gray,
        Tip,
    }
}
