using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public interface IMove2D : IBeginDragHandler, IDragHandler, IEndDragHandler {

}


public interface ICardFight {


}

public interface IHandToHandFight : ICardFight {

    void HandToHandFight(BaseObject _target);
}

public interface IMagicAttack : ICardFight {

    void SingleTargetMagicAttack(BaseObject _target);

    void AOEMagicAttack(List<BaseObject> _targetList);
}

public interface IBuffCast : ICardFight {



}


public interface ICardProperty {

    int HealthPoint {
        get;
        set;
    }

    int AttackPoint {
        get;
        set;
    }


}