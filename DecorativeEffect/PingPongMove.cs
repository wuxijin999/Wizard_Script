using UnityEngine;
using System.Collections;

public class PingPongMove : MonoBehaviour {

    public Transform reference;

    public float amplitudeX;
    public float amplitudeY;
    public float amplitudeZ;

    public float speed;

    Vector3 referencePos;
    void Start() {
        if (reference == null) {
            referencePos = this.transform.localPosition;
        }
        else {
            referencePos = reference.localPosition;
        }

    }


    float newX;
    float newY;
    float newZ;
    void LateUpdate() {

        if (Mathf.Abs(amplitudeX) > 0.001f) {
            newX = referencePos.x - amplitudeX * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeX);
        }
        else {
            newX = referencePos.x;
        }

        if (Mathf.Abs(amplitudeY) > 0.001f) {
            newY = referencePos.y - amplitudeY * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeY);
        }
        else {
            newY = referencePos.y;
        }

        if (Mathf.Abs(amplitudeZ) > 0.001f) {
            newZ = referencePos.z - amplitudeZ * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeZ);
        }
        else {
            newZ = referencePos.z;
        }

        this.transform.localPosition = new Vector3(newX,newY,newZ);
    }


}
