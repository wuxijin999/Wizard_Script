using UnityEngine;
using System.Collections;

namespace Fight {

    public class FloatStateProcessor : BodyStateProcessor {

        Transform transform;
        FloatData floatData;

        float startY;
        float targetY;
        bool isRise = false;
        float startTime = 0f;
        float riseTimer = 0f;


        public void Begin(FloatData _floatData) {
            isRise = true;
            startTime = Time.time;
            riseTimer = 0f;
            startY = transform.position.y;
            targetY = startY + _floatData.Height;
        }

        public override void DoAction() {
            base.DoAction();

            if (isRise) {
                float y = Mathf.Lerp(startY, targetY, (Time.time - startTime) / riseTimer);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if (transform.position.y > targetY) {
                    isRise = false;
                }
            }

            float groundY = 0f;
            if (!isRise) {

            }

        }


    }
}

