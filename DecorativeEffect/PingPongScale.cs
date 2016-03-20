using UnityEngine;
using System.Collections;

public class PingPongScale : MonoBehaviour {

    public Transform reference;

    public float amplitudeX;
    public float amplitudeY;
    public float amplitudeZ;

    public float speed;
    Vector3 referenceScale;

    void Start() {
        if (reference == null) {
            referenceScale = this.transform.localScale;
        }
        else {
            referenceScale = reference.localScale;
        }

    }


    float newX;
    float newY;
    float newZ;
    void LateUpdate() {

        if (Mathf.Abs(amplitudeX) > 0.001f) {
            newX = referenceScale.x - amplitudeX * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeX);
        }
        else {
            newX = referenceScale.x;
        }

        if (Mathf.Abs(amplitudeY) > 0.001f) {
            newY = referenceScale.y - amplitudeY * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeY);
        }
        else {
            newY = referenceScale.y;
        }

        if (Mathf.Abs(amplitudeZ) > 0.001f) {
            newZ = referenceScale.z - amplitudeZ * 0.5f + Mathf.PingPong(Time.time * speed, amplitudeZ);
        }
        else {
            newZ = referenceScale.z;
        }

        this.transform.localScale = new Vector3(newX,newY,newZ);
    }


}
