using UnityEngine;
using UI;

public class UIRoot : MonoBehaviour {

    [SerializeField]
    private RectTransform m_NormalCanvas;
    public RectTransform normalCanvas {
        get {
            return m_NormalCanvas;
        }
    }
    [SerializeField]
    private RectTransform m_ModalCanvas;
    public RectTransform modalCanvas {
        get {
            return m_ModalCanvas;
        }
    }
    [SerializeField]
    private RectTransform m_TipsCanvas;
    public RectTransform tipsCanvas {
        get {
            return m_TipsCanvas;
        }
    }
    [SerializeField]
    private RectTransform m_SystemCanvas;
    public RectTransform systemCanvas {
        get {
            return m_SystemCanvas;
        }
    }

    void Awake () {
        WindowManager.Instance.uiRoot = this;
    }
}
