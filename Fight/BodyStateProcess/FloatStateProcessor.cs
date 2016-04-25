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

        float freeFallTimer = 0f;
        public void Begin(FloatData _floatData, ActorTransform _actorTransform) {
            startTime = Time.time;
            riseTimer = _floatData.RiseTime;
            actorTransform = _actorTransform;
            startY = actorTransform.transform.position.y;
            targetY = startY + _floatData.Height;

            stage = FloatStage.Rise;
            actorTransform.HeightSyncable = false;
        }

        public override void DoAction() {
            base.DoAction();
            float y = 0f;
            switch (stage) {
                case FloatStage.None:
                    break;
                case FloatStage.Rise:
                    y = Mathf.Lerp(startY, targetY, Mathf.Clamp01((Time.time - startTime) / riseTimer));
                    actorTransform.transform.position = actorTransform.transform.position.SetY(y);
                    if (actorTransform.transform.position.y > (targetY - 0.0001f)) {
                        stage = FloatStage.Fall;
                        freeFallTimer = 0f;
                    }
                    break;
                case FloatStage.Fall:
                    freeFallTimer += Time.deltaTime;
                    y = MathfTools.FreeFall(targetY, freeFallTimer);
                    actorTransform.transform.position = actorTransform.transform.position.SetY(y);

                    if (actorTransform.IsUnderGround()) {
                        stage = FloatStage.None;
                        actorTransform.HeightSyncable = true;

                        End();
                    }
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

