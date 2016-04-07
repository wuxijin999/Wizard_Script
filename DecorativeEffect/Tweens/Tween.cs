using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour {

    public AnimationCurve curve;
    public Vector3 from;
    public Vector3 to;
    public float delay;
    public float duration;
    public Trigger trigger;
    public WrapMode wrapMode;

    protected float accumulatedTime;
    protected float curveLength;
    protected bool doTween = false;

    protected float t = 0f;
    protected float value = 0f;
    protected Vector3 newVector3 = Vector3.one;

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

        if (doTween && duration > 0.001f) {
            accumulatedTime += Time.deltaTime;
            ApplyNewVector3();
        }
    }

    IEnumerator Co_StartTween() {
        if (delay < 0f) {
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

    protected Vector3 CalculateVector3() {

        Vector3 newVector3 = Vector3.zero;
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
        newVector3 = Vector3.Lerp(from, to, value);

        switch (wrapMode) {
            case WrapMode.Once:
                if (t > 1f) {
                    doTween = false;
                }
                break;
        }

        return newVector3;
    }

    protected virtual void ApplyNewVector3() {

    }

    public enum Trigger {
        Start,
        Enable,
    }

    public enum WrapMode {
        Once,
        Loop,
        PingPong,
    }
}

