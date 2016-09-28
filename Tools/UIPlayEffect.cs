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
    [SerializeField]
    private int m_EffectId;
    public int effectId {
        get { return m_EffectId; }
        set { m_EffectId = value; }
    }

    [SerializeField]
    private int m_Sortingorder;
    public int sortingOrder { get { return m_Sortingorder; } }

    private float m_Delay;
    public float delay {
        get { return Mathf.Clamp(m_Delay, 0, float.MaxValue); }
        set { m_Delay = value; }
    }

    private AutoType type;
    float triggerTime = 0f;

    #endregion

    public void Play () {
        triggerTime = Time.time;
    }

    #region Built-In

    void Awake () {

    }

    void OnEnable () {
        if (type == AutoType.OnEnable) {
            triggerTime = Time.time + delay;
        }
    }

    void Start () {
        if (type == AutoType.OnStart) {
            triggerTime = Time.time + delay;
        }
    }

    void Update () {
        if (Time.time >= triggerTime) {
            PlayEffect();
            triggerTime = float.MaxValue;
        }
    }


    private void PlayEffect () {
        RefEffect effect;
        if (RefEffect.TryGet(effectId, out effect)) {
            this.gameObject.PlayEffect(effect.resourceName).OnComplete(null);
        }
    }

    #endregion

}



