using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEx : Button {

    public float ForbidDuration = 1f;


    float forbidTimer = 0f;
    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);

        forbidTimer = 1f;
        interactable = false;
    }


    void Update() {
        if (forbidTimer > 0) {
            forbidTimer -= Time.deltaTime;
            if (forbidTimer < 0f) {
                interactable = true;
            }
        }
    }


}
