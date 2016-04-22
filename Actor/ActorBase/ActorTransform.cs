using UnityEngine;
using System.Collections;

public class ActorTransform : MonoBehaviour {

    CharacterController controller;
    protected virtual void Awake() {
        controller = this.GetComponent<CharacterController>();
    }

    protected virtual void OnEnable() {
    }

    protected virtual void Start() {
    }

    protected virtual void FixedUpdate() {

    }
    protected virtual void Update() {
    }

    protected virtual void LateUpdate() {
        UpdateHeight();
    }

    protected virtual void OnDisable() {

    }

    protected virtual void OnDestroy() {

    }

    private void UpdateHeight() {

        Ray ray = new Ray(this.transform.position.AddY(30), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 50, Layer.GroundMask);
        if (hit.collider != null) {
            this.transform.position = this.transform.position.SetY(hit.point.y + controller.height * 0.5f);
        }
    }
}
