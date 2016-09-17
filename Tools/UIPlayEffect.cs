//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Thursday, September 15, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

public class UIPlayEffect : MonoBehaviour {

    public enum AutoType {
        None,
        OnStart,
        OnEnable,
    }

    #region Fields

    private int m_EffectId;
    public int effectId {
        get {
            return m_EffectId;
        }
        set {
            m_EffectId = value;
        }
    }

    [SerializeField]
    private int m_Sortingorder;
    public int sortingOrder {
        get {
            return m_Sortingorder;
        }
    }

    private float m_Delay;
    public float delay {
        get {
            return m_Delay;
        }
        set {
            m_Delay = value;
        }
    }

    private AutoType type;

    #endregion

    #region Built-In
    void Awake () {

    }

    void Start () {

    }

    void Update () {

    }


    private void PlayEffect () {

    }
    #endregion

}



