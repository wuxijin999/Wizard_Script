//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Sunday, September 18, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupMenus : MonoBehaviour {

    #region Fields

    public RectTransform rectTransform {
        get {
            return this.transform as RectTransform;
        }
    }

    [SerializeField]
    private RectTransform m_Content;
    public RectTransform content {
        get {
            return m_Content;
        }
        set {
            m_Content = value;
        }
    }

    public ButtonEx buttonSwitch;
    public ButtonEx[] buttonSet;

    #endregion

    #region Built-In
    void Awake () {

    }

    void Start () {

    }

    void Update () {

    }

    private void UnFold () {

    }

    private void Fold () {

    }

    #endregion

}



