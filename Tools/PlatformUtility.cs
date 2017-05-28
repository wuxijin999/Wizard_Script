using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class UnityPartial {

    static public bool IsEditor () {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    static public bool IsAndroidMobile () {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }


}