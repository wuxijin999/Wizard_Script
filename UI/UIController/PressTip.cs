using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PressTip : MonoBehaviour {

    RectTransform mRect;

    public void SetContent(string _content) {



    }

    private Vector3 CalculatePosition(Vector3 _mousePosition) {

        Vector3 screenPosition = CameraTool.ScreenToUIPosition(_mousePosition);


        return screenPosition;
    }

}
