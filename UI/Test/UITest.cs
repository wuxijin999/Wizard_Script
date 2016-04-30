using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class UITest : MonoBehaviour {

    public Dictionary<int, Component> hashComponent = new Dictionary<int, Component>();
    public Dictionary<int, IPointerClickHandler> hashClick = new Dictionary<int, IPointerClickHandler>();
    public Dictionary<int, IPointerDownHandler> hashDown = new Dictionary<int, IPointerDownHandler>();
    public Dictionary<int, IPointerUpHandler> hashUp = new Dictionary<int, IPointerUpHandler>();
    public Dictionary<int, ISubmitHandler> hashSubmit = new Dictionary<int, ISubmitHandler>();

    public Dictionary<int, int> clickTestTimes = new Dictionary<int, int>();
    public Dictionary<int, int> downTestTimes = new Dictionary<int, int>();
    public Dictionary<int, int> upTestTimes = new Dictionary<int, int>();
    public Dictionary<int, int> submitTestTimes = new Dictionary<int, int>();

    [System.NonSerialized]
    public int TouchCount = 1;
    [System.NonSerialized]
    public bool auto = false;
    [System.NonSerialized]
    public float interVal = 1f;
    [System.NonSerialized]
    public float autoCumulativeTime = 0f;
    private float timer = 0f;

    List<int> keys = new List<int>();

    IPointerClickHandler clickHandler = null;
    IPointerDownHandler downHandler = null;
    IPointerUpHandler upHandler = null;
    ISubmitHandler submitHandler = null;

    void Update() {

        if (!auto) {
            return;
        }

        autoCumulativeTime += Time.deltaTime;

        if (timer < interVal) {
            timer += Time.deltaTime;
            return;
        }

        timer = 0f;

        for (int i = 0; i < TouchCount; i++) {
            if (keys == null || keys.Count == 0) {
                break;
            }
            int index = UnityEngine.Random.Range(0, keys.Count);
            int key = keys[index];

            if (hashClick.TryGetValue(key, out clickHandler)) {
                clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
                clickTestTimes[key]++;
            }

            if (hashDown.TryGetValue(key, out downHandler)) {
                downHandler.OnPointerDown(new PointerEventData(EventSystem.current));
                downTestTimes[key]++;
            }

            if (hashUp.TryGetValue(key, out upHandler)) {
                upHandler.OnPointerUp(new PointerEventData(EventSystem.current));
                upTestTimes[key]++;
            }

            if (hashSubmit.TryGetValue(key, out submitHandler)) {
                submitHandler.OnSubmit(new PointerEventData(EventSystem.current));
                submitTestTimes[key]++;
            }
        }

    }

    public void BindController() {
        hashComponent.Clear();
        hashClick.Clear();
        hashDown.Clear();
        hashUp.Clear();
        hashSubmit.Clear();

        clickTestTimes.Clear();
        downTestTimes.Clear();
        upTestTimes.Clear();
        submitTestTimes.Clear();

        SeekChildComponent(this.transform);
        keys = new List<int>(hashComponent.Keys);
    }

    public void StartAutoTest() {
        auto = true;
        autoCumulativeTime = 0f;
    }

    public void StopAutoTest() {
        auto = false;
        autoCumulativeTime = 0f;
    }

    private void SeekChildComponent(Transform _transform) {
        if (_transform == null) {
            return;
        }
        Component component = null;
        IPointerClickHandler click = null;
        IPointerDownHandler down = null;
        IPointerUpHandler up = null;
        ISubmitHandler submit = null;
        Transform transform = null;

        for (int i = 0; i < _transform.childCount; i++) {
            transform = _transform.GetChild(i);
            click = transform.GetComponent<IPointerClickHandler>();
            down = transform.GetComponent<IPointerDownHandler>();
            up = transform.GetComponent<IPointerUpHandler>();
            submit = transform.GetComponent<ISubmitHandler>();

            if (click != null) {
                component = click as Component;
                hashClick[click.GetHashCode()] = click;
                clickTestTimes[click.GetHashCode()] = 0;
                hashComponent[click.GetHashCode()] = component;
            }

            if (down != null) {
                component = down as Component;
                hashDown[down.GetHashCode()] = down;
                downTestTimes[down.GetHashCode()] = 0;
                hashComponent[down.GetHashCode()] = component;
            }

            if (up != null) {
                component = up as Component;
                hashUp[up.GetHashCode()] = up;
                upTestTimes[up.GetHashCode()] = 0;
                hashComponent[up.GetHashCode()] = component;
            }

            if (submit != null) {
                component = submit as Component;
                hashSubmit[submit.GetHashCode()] = submit;
                submitTestTimes[submit.GetHashCode()] = 0;
                hashComponent[submit.GetHashCode()] = component;
            }

            SeekChildComponent(_transform.GetChild(i));
        }

    }


}
