using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiscTools {

    static public List<T> GetComponentsInProgeny<T>(Transform _t) {
        List<T> ret = new List<T>();

        if (_t.childCount > 0) {
            for (int i = 0; i < _t.childCount; i++) {
                ret.AddRange(GetComponentsInProgeny<T>(_t.GetChild(i)));
            }
        }
        else {
            ret.AddRange(_t.GetComponentsInChildren<T>(true));
        }

        return ret;
    }

}
