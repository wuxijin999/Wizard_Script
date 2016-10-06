using UnityEngine;

public class UIGroup : MonoBehaviour, ICanvasRaycastFilter {

    [Tooltip("Is Accept Raycast")]
    [SerializeField]
    private bool m_RaycastTarget = true;
    public bool raycastTarget { get { return m_RaycastTarget; } set { m_RaycastTarget = value; } }

    public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera) {
        return raycastTarget;
    }

    public void SetActive (bool _active) {
        this.gameObject.SetActive(_active);
    }

}
