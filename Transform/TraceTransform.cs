using UnityEngine;
using System.Collections;

public class TraceTransform : MonoBehaviour {

    public Transform target;
    public float speed;

    Vector3 targetPosition {
        get {
            if (target != null) {
                return target.position;
            }
            else {
                return Vector3.zero;
            }
        }
    }

    void Start() {

    }

    void LateUpdate() {
        if (target == null) {
            return;
        }

        if (Vector3.Distance(targetPosition, this.transform.position) < 0.001f) {
            return;
        }

        MoveToTargetPosition();
    }

    private void MoveToTargetPosition() {
        this.transform.LookAt(target);
        float distance = Vector3.Distance(targetPosition, this.transform.position);
        float t = Mathf.Clamp01(speed * Time.deltaTime / distance);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, t);
    }
}
