using UnityEngine;

namespace Fight {

    public interface ILife {
        void Born();
        void Dead();
    }
    public interface IMove {

        void MoveTo(Vector3 position);
        void MoveTo(Vector3 position, float speed);
        void MoveTo(Vector3 position, float speed, float acceleration);
        void MoveTo(Transform transform, float speed);
        void MoveTo(Transform transform, float speed, float acceleration);
        void MoveStep(Vector3 deltaVector3);
        void RotateTo(Quaternion _rotation);
        void Follow(Transform transform, Vector3 relativePosition);
        void StopFollow();
    }

    public enum FightPropertyType {
        Hp,
        MaxHp,
        Attack,
        Defence,
        MoveSpeed,
        AttackSpeed,

    }
    public interface IProperty {

        int QueryProperty(FightPropertyType propertyType);
        void IncreaseProperty(FightPropertyType propertyType, int value);
        void DecreaseProperty(FightPropertyType propertyType, int value);
    }

    public interface IAttack {

        void CastSkill(int skillId, Actor target);
        void CastSkill(int skillId, Vector3 position);

    }

    public interface IHurt {

        void DealHurt();

    }

}
