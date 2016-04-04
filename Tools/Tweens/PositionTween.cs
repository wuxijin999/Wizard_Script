using UnityEngine;
using System.Collections;

public class PositionTween : MonoBehaviour {

    public AnimationCurve curve;
    public Vector3 from;
    public Vector3 to;
    public float delay;
    public float duration;

    public enum Trigger {
        Start,
        Enable,
    }

    public Trigger trigger;

    public enum WrapMode {
        Once,
        Loop,
        PingPong,
    }
    public WrapMode wrapMode;

    float accumulatedTime;
    float curveLength;
    bool doTween = false;

    float t = 0f;
    float value = 0f;
    Vector3 newPosition = Vector3.zero;

    void Start() {
        if (trigger == Trigger.Start) {
            StartCoroutine(Co_StartTween());
        }
    }

    void OnEnable() {
        if (trigger == Trigger.Enable) {
            StartCoroutine(Co_StartTween());
        }
    }

    void LateUpdate() {

        if (doTween) {
            DoTween(Time.deltaTime);
        }
    }

    IEnumerator Co_StartTween() {
        if (delay < 0.001f) {
            Debug.LogError("Delaytime should not be less than zero!");
            yield break;
        }
        if (duration < 0.001f) {
            Debug.LogError("Duration should not be less than zero!");
            yield break;
        }

        if (curve.keys.Length < 2) {
            curve = AnimationCurve.Linear(0, 0, 1, 1);
        }

        doTween = false;
        yield return new WaitForSeconds(delay);
        curveLength = curve.keys[curve.keys.Length - 1].time - curve.keys[0].time;
        doTween = true;
        accumulatedTime = 0f;
    }

    void DoTween(float _deltaTime) {
        accumulatedTime += _deltaTime;
        switch (wrapMode) {
            case WrapMode.Once:
                t = accumulatedTime * curveLength / duration;
                break;
            case WrapMode.Loop:
                t = Mathf.Repeat(accumulatedTime * curveLength / duration, 1);
                break;
            case WrapMode.PingPong:
                t = Mathf.PingPong(accumulatedTime * curveLength / duration, 1);
                break;
        }

        value = curve.Evaluate(t);

        newPosition = Vector3.Lerp(from, to, value);
        this.transform.localPosition = newPosition;
        switch (wrapMode) {
            case WrapMode.Once:
                if (t > 1f) {
                    doTween = false;
                }
                break;
        }
    }
}
