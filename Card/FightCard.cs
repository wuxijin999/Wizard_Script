using UnityEngine;
using System.Collections;

public class FightCard : CardBase, ICardFight, ICardProperty {



    #region ICardProperty 成员

    public int HealthPoint {
        get {
            throw new System.NotImplementedException();
        }
        set {
            throw new System.NotImplementedException();
        }
    }

    public int AttackPoint {
        get {
            throw new System.NotImplementedException();
        }
        set {
            throw new System.NotImplementedException();
        }
    }

    #endregion
}
