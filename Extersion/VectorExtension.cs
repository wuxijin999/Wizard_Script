using UnityEngine;
using System.Collections;

public static class VectorExtension {


    public static Vector3 SetX(this Vector3 _v3, float _x) {
        _v3.x = _x;
        return _v3;
    }

    public static Vector3 SetY(this Vector3 _v3, float _y) {
        _v3.y = _y;
        return _v3;
    }

    public static Vector3 SetZ(this Vector3 _v3, float _z) {
        _v3.z = _z;
        return _v3;
    }

    public static Vector3 AddX(this Vector3 _v3, float _offsetX) {
        _v3.x += _offsetX;
        return _v3;
    }

    public static Vector3 AddY(this Vector3 _v3, float _offsetY) {
        _v3.y += _offsetY;
        return _v3;
    }

    public static Vector3 AddZ(this Vector3 _v3, float _offsetZ) {
        _v3.z += _offsetZ;
        return _v3;
    }
}
