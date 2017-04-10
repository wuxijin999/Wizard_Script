using UnityEngine;
using System.Collections;


public class ClampInt {
    int min;
    int max;

    private int mValue;
    public int value {
        get {
            return mValue;
        }
        set {
            mValue = Mathf.Clamp(value, min, max);
        }
    }

    public ClampInt(int _value, int _min = int.MinValue, int _max = int.MaxValue) {
        mValue = _value;
        min = _min;
        max = _max;
    }
}


    public class ClampFloat {
        float min;
        float max;

        private float mValue;
        public float value {
            get {
                return mValue;
            }
            set {
                mValue = Mathf.Clamp(value, min, max);
            }
        }

        public ClampFloat(float _value, float _min = float.MinValue, float _max = float.MaxValue) {
            mValue = _value;
            min = _min;
            max = _max;
        }
    }
