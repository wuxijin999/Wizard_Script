using UnityEngine;
using System.Collections;

public class PingPongRotation : MonoBehaviour {

    public Transform reference;

    public float amplitudeX;
    public float amplitudeY;
    public float amplitudeZ;

    public float speed;
    Vector3 referenceEuler;
    void Start() {
        if (reference == null) {
            referenceEuler = this.transform.localEulerAngles;
        }
        else {
            referenceEuler = reference.localEulerAngles;
        }

    }


    float newX;
    float newY;
    float newZ;
    void LateUpdate() {

        if (Mathf.Abs(amplitudeX) > 0.001f) {
            newX = referenceEuler.x - amplitudeX * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeX);
        }
        else {
            newX = referenceEuler.x;
        }

        if (Mathf.Abs(amplitudeY) > 0.001f) {
            newY = referenceEuler.y - amplitudeY * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeY);
        }
        else {
            newY = referenceEuler.y;
        }

        if (Mathf.Abs(amplitudeZ) > 0.001f) {
            newZ = referenceEuler.z - amplitudeZ * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeZ);
        }
        else {
            newZ = referenceEuler.z;
        }

        this.transform.localEulerAngles = new Vector3(newX,newY,newZ);
    }


}
