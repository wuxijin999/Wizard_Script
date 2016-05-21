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
        TestBiz biz = null;
        #endregion

        #region Built-in
        protected override void BindController () {
            base.BindController();
        }

        protected override void OnPreOpen () {
            base.OnPreOpen();
            if (biz == null) {
                biz = new TestBiz();
            }
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




