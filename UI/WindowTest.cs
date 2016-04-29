using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class WindowTest : MonoBehaviour {

    List<IPointerClickHandler> clickHandler = new List<IPointerClickHandler>();

    public void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            BindController();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            for (int i = 0; i < clickHandler.Count; i++) {
                clickHandler[i].OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }

    public void BindController() {
        buttonList.Clear();
        clickHandler.Clear();

        SeekChildComponent(this.transform);
    }


    private void SeekChildComponent(Transform _transform) {
        if (_transform == null) {
            return;
        }
        IPointerClickHandler iPointerClick;

        for (int i = 0; i < _transform.childCount; i++) {
            iPointerClick = _transform.GetChild(i).GetComponent<IPointerClickHandler>();
            if (iPointerClick != null) {
                clickHandler.Add(iPointerClick);
            }
            SeekChildComponent(_transform.GetChild(i));
        }

    }


}
