//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Friday, May 27, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;


public class EffectCtrl {

    public Action onComplte = null;

    private bool loop = false;
    public bool Loop {
        get {
            return loop;
        }
        set {
            loop = value;
        }
    }


}

public static class SFXExtersion {

    static public EffectCtrl PlayEffect (this GameObject gameObject, string effectName) {
        EffectCtrl effect = new EffectCtrl();

        return effect;
    }

    static public EffectCtrl SetLoop (this EffectCtrl effect, bool _loop) {
        effect.Loop = _loop;
        return effect;
    }

    static public void OnComplete (this EffectCtrl effect, Action _callBack) {
        effect.onComplte = _callBack;
    }

}





