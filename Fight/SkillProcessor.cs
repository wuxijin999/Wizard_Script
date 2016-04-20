using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight {
    public class SkillProcessor {

        Skill skill = null;
        public SkillProcessor(Skill _skill) {
            skill = _skill;

        }


        List<Actor> hitedActorList = new List<Actor>();
        List<Collider> hitedColliderList = new List<Collider>();
        Collider[] tempColliderSet = null;
        /// <summary>
        /// 攻击判定
        /// </summary>
        public void AttackDecision(int _hitId) {
            HitData hitData = BattleManager.Instance.QueryHitData(_hitId);
            Vector3 center;
            float radius = 0f;

            hitedColliderList.Clear();

            for (int i = 0; i < hitData.decisionSpheres.Length; i++) {
                center = hitData.decisionSpheres[i].Center;
                radius = hitData.decisionSpheres[i].Radius;
                tempColliderSet = Physics.OverlapSphere(center, radius, LayerMask.NameToLayer("Actor"));

                for (int j = 0; j < tempColliderSet.Length; j++) {
                    if (!hitedColliderList.Contains(tempColliderSet[i])) {
                        hitedColliderList.Add(tempColliderSet[i]);
                    }
                }
            }

            Actor a = null;
            hitedActorList.Clear();
            for (int i = 0; i < hitedColliderList.Count; i++) {
                a = hitedColliderList[i].GetComponent<Actor>();
                hitedActorList.Add(a);
            }

            BattleManager.Instance.ProcessHitedActor(hitedActorList);

        }

    }
}

