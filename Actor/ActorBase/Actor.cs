using UnityEngine;
using System.Collections;
using System;

namespace Fight {

    public class Actor : ILife {

        protected int instanceId;
        public int InstanceId {
            get {
                return instanceId;
            }
        }

        protected int level;
        public int Level {
            get {
                return level;
            }
        }

        protected ActorTransform actorTransform;
        public ActorTransform aTransform {
            get {
                return actorTransform;
            }
        }

        public Actor(ActorTransform _transform) {
            instanceId = BattleManager.Instance.AllocateActorInstanceId();
            actorTransform = _transform;
        }

        public virtual void Born() {

        }

        public virtual void Dead() {

        }
    }


}

