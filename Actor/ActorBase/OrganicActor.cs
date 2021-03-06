﻿using UnityEngine;
using System.Collections;
using System;

namespace Fight {
    public class OrganicActor : Actor, IMove {

        public OrganicActor(ActorTransform _transform) : base(_transform) {

        }

        public override void Born() {
            base.Born();
        }

        public override void Dead() {
            base.Dead();
        }

        public void Follow(Transform transform, Vector3 relativePosition) {
            throw new NotImplementedException();
        }

        public void MoveStep(Vector3 deltaVector3) {
            this.actorTransform.transform.position += deltaVector3;
        }

        public void MoveTo(Vector3 position) {
            throw new NotImplementedException();
        }

        public void MoveTo(Transform transform, float speed) {
            throw new NotImplementedException();
        }

        public void MoveTo(Vector3 position, float speed) {
            throw new NotImplementedException();
        }

        public void MoveTo(Transform transform, float speed, float acceleration) {
            throw new NotImplementedException();
        }

        public void MoveTo(Vector3 position, float speed, float acceleration) {
            throw new NotImplementedException();
        }

        public void RotateTo(Quaternion _rotation) {
            actorTransform.transform.rotation = _rotation;
        }

        public void StopFollow() {
            throw new NotImplementedException();
        }





    }

}

