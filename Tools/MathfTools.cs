using UnityEngine;

public class MathfTools {

    /// <summary>
    /// 返回手势方向
    /// </summary>
    /// <param name="_startPos"></param>
    /// <param name="_endPos"></param>
    /// <returns></returns>
    static public GestureType CalGestureDirection(Vector2 _startPos, Vector2 _endPos) {
        GestureType type;
        Vector2 slideDirection = _endPos - _startPos;
        float x = slideDirection.x, y = slideDirection.y;
        if (y < x && y > -x) {
            type = GestureType.Right;
        }
        else if (y > x && y < -x) {
            type = GestureType.Left;
        }
        else if (y > x && y > -x) {
            type = GestureType.Up;
        }
        else {
            type = GestureType.Down;
        }

        return type;
    }
}
