using UnityEngine;
using System.Collections;

public class WindowInfo : MonoBehaviour {

    public delegate void WinAnimationHandler();
    public event WinAnimationHandler WinOpenCompleteEvent;
    public event WinAnimationHandler WinCloseCompleteEvent;

    public WindowType Type;
    public WinAnimType AnimType;
    public WinOffsetAnimStyle OffsetStyle;

    public bool ClickEmptyToClose = false;
    public bool NeedMask = true;
    public int MaskAlpha = 128;

    private void OnOpenComplete() {

        if (WinOpenCompleteEvent != null) {
            WinOpenCompleteEvent();
        }
    }


    private void OnCloseComplete() {

        if (WinCloseCompleteEvent != null) {
            WinCloseCompleteEvent();
        }
    }


}
