using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBase : BaseObject, IMove2D {


    public virtual void DoMove() {

    }

    /// <summary>
    /// 卡牌初始化
    /// </summary>
    protected virtual void Init() {

    }


    #region IBeginDragHandler 成员

    public void OnBeginDrag(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }

    #endregion

    #region IDragHandler 成员

    public void OnDrag(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }

    #endregion

    #region IEndDragHandler 成员

    public void OnEndDrag(PointerEventData eventData) {
        throw new System.NotImplementedException();
    }

    #endregion
}
