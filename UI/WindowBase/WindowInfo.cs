//------------------------------------------------------------------
//                              [Author]:Wu Xijin
//
//--------------------------------------------------------------------
using UnityEngine;
using System.Collections;

namespace UI {

    public class WindowInfo : MonoBehaviour, ICanvasRaycastFilter {

        public delegate void WinAnimationHandler ();
        public event WinAnimationHandler WinOpenCompleteEvent;
        public event WinAnimationHandler WinCloseCompleteEvent;

        public WindowType Type;
        public WinAnimType AnimType;
        public WinOffsetAnimStyle OffsetStyle;

        public bool ClickEmptyToClose = false;
        public bool NeedMask = true;
        public int MaskAlpha = 128;

        public bool IsRaycastValid {
            get; set;
        }

        public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera) {
            return IsRaycastValid;
        }

        private void OnOpenComplete () {

            if (WinOpenCompleteEvent != null) {
                WinOpenCompleteEvent();
            }
        }

        private void OnCloseComplete () {

            if (WinCloseCompleteEvent != null) {
                WinCloseCompleteEvent();
            }
        }

    }

}

