using UnityEngine;
using System.Collections;

namespace Fight {

    public class FloatStateProcessor : BodyStateProcessor {

        ActorTransform actorTransform;
        FloatData floatData;

        float startY;
        float targetY;
        float startTime = 0f;
        float riseTimer = 0f;
        FloatStage stage;

        public void Begin(FloatData _floatData) {
            startTime = Time.time;
            riseTimer = _floatData.RiseTime;
            startY = actorTransform.transform.position.y;
            targetY = startY + _floatData.Height;

            stage = FloatStage.Rise;
            actorTransform.HeightSyncable = false;
        }

        public override void DoAction() {
            base.DoAction();

            switch (stage) {
                case FloatStage.None:
                    break;
                case FloatStage.Rise:
                    float y = Mathf.Lerp(startY, targetY, Mathf.Clamp01((Time.time - startTime) / riseTimer));
                    actorTransform.transform.position = actorTransform.transform.position.SetY(y);
                    if (actorTransform.transform.position.y > targetY) {
                        stage = FloatStage.Fall;
                    }
                    break;
                case FloatStage.Fall:

                    break;
            }


        }

        public enum FloatStage {
            None,
            Rise,
            Fall,
        }
    }
}

