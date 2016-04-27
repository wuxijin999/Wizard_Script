using UnityEngine;
using System.Collections;

public class WDebug {

    public static void Log(string _content) {
        Debug.Log(_content);
    }

    public static void Waring(string _content) {
        Debug.LogWarning(_content);
    }

    public static void Error(string _content) {
        Debug.LogError(_content);
    }
}
