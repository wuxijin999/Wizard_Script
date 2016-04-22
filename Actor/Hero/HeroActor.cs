using UnityEngine;
using System.Collections;
using System;

namespace Fight {
    public class HeroActor : OrganicActor, IAttack {

        HandShankResponser handShankResponser;

        public HeroActor(ActorTransform _transform)
            : base(_transform) {
            handShankResponser = new HandShankResponser(HandShank.Instance);
            handShankResponser.iMove = this;
            handShankResponser.speed = 5f;
        }

        public void CastSkill(int skillId, Vector3 position) {
            Skill skill = new Skill(skillId);

        }

        public void CastSkill(int skillId, Actor target) {
            Skill skill = new Skill(skillId);

        }

    }

}

