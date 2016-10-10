//-----------------------------------------------------------------
//         [Author]: Leonard.Wu 
//         [Date]: Saturday, May 21, 2016  
//-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace UI {

    public class TestWin : WindowViewBase {

        #region Member
        Button btnClick;
        #endregion

        #region Built-in
        protected override void BindController () {
            base.BindController();
            btnClick = panel.GetComponentByPath<Button>("Image");
        }

        protected override void AddListeners () {
            base.AddListeners();
            btnClick.onClick.AddListener(() => {
                WDebug.Log("fffff");
            }
            );
        }

        protected override void OnPreOpen () {
            base.OnPreOpen();

        }

        protected override void OnAfterOpen () {
            base.OnAfterOpen();
        }

        protected override void OnPreClose () {
            base.OnPreClose();
        }

        protected override void OnAfterClose () {
            base.OnAfterClose();

        }
        #endregion

        #region Interaction
        #endregion

        #region CallBack
        #endregion

        #region Functional Method
        #endregion
    }

}




