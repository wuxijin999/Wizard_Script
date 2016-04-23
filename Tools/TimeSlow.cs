using UnityEngine;
using System.Collections;

public class TimeSlow : MonoBehaviour {

    float targetAmount = float.MaxValue;
    float startAmount = 0f;
    float duration = 0f;
    float timer = 0f;

    TimeSlowStage stage = TimeSlowStage.None;

    public void StartTimeSlot(float _amount, float _duration) {

        if (!(_duration > 0f) || _amount > targetAmount) {
            return;
        }

        targetAmount = Mathf.Clamp(_amount, 0.2f, 1f);
        startAmount = Time.timeScale;
        duration = _duration;
        timer = 0f;
        stage = TimeSlowStage.SlowDown;
    }


    void OnEnable() {

    }

    void LateUpdate() {
        switch (stage) {
            case TimeSlowStage.None:
                break;
            case TimeSlowStage.SlowDown:
                if (timer < duration) {
                    timer += Time.deltaTime;
                    float s = Mathf.Lerp(startAmount, targetAmount, Mathf.Clamp01(timer / duration));
                    Time.timeScale = s;
                    if (timer > duration) {
                        timer = 0f;
                        stage = TimeSlowStage.Recover;
                    }
                }
                break;
            case TimeSlowStage.Recover:
                if (timer < duration) {
                    timer += Time.deltaTime;
                    float s = Mathf.Lerp(targetAmount, 1f, Mathf.Clamp01(timer / duration));
                    Time.timeScale = s;
                    if (timer > duration) {
                        timer = 0f;
                        stage = TimeSlowStage.None;
                    }
                }
                break;
        }

    }

    void OnDisable() {
        Time.timeScale = 1f;
        stage = TimeSlowStage.None;
    }

    void OnDestroy() {

    }


    public enum TimeSlowStage {
        None,
        SlowDown,
        Recover,
    }

}
