using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultipProgress : MonoBehaviour {

    public Slider SliderMain;
    public Slider SliderFollow;

    public Image imgFore;
    public Image imgMid;
    public Image imgBg;

    public float targetValue = 0f;
    public float smoothTime01 = 0.2f;
    public float smoothTime02 = 0.3f;

    float refV01 = 0f;
    float refV02 = 0f;

    public void Init(string _spriteFore, string _spriteMid, string _spriteBg) {
    }

    public void SetValue(float _value) {

        targetValue = _value;
        targetValue = Mathf.Clamp01(targetValue);
    }

    void Update() {
        if (Mathf.Abs(SliderMain.value - targetValue) < 0.0001f && Mathf.Abs(SliderFollow.value - targetValue) < 0.0001f) {
            return;
        }

        if (targetValue > SliderMain.value) {
            SliderFollow.value = Mathf.SmoothDamp(SliderFollow.value, targetValue, ref refV02, smoothTime01);
            SliderMain.value = Mathf.SmoothDamp(SliderMain.value, targetValue, ref refV01, smoothTime02);
        }
        else {
            SliderMain.value = Mathf.SmoothDamp(SliderMain.value, targetValue, ref refV01, smoothTime01);
            SliderFollow.value = Mathf.SmoothDamp(SliderFollow.value, targetValue, ref refV02, smoothTime02);
        }

    }

}
