using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMove:MonoBehaviour {

    public AnimationCurve curve;
    public AnimationCurve normalCurve;
    public float durarion;
    public float normalRatio;

    public Transform from;
    public Transform to;
    public Transform ball;

    float timer = 0f;

    private void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.F)) {
            StartCoroutine(Co_CurveMove());
        }
    }

    IEnumerator Co_CurveMove() {

        Vector3 nl = Vector3.Normalize(to.position - from.position);
        Vector3 normal = new Vector3(-nl.y,nl.x,0);
        float distance = Vector3.Distance(from.position,to.position);
        Debug.Log(string.Format("from:{0},to:{1}",from.position,to.position));
        timer = 0f;
        while(timer < durarion) {
            timer += Time.deltaTime;
            var t = Mathf.Clamp01(timer / durarion);
            var c1 = curve.Evaluate(t);
            var c2 = normalCurve.Evaluate(t) * normalRatio;
            Vector3 v1 = (to.position - from.position) * c1 + from.position;
            Vector3 v2 = normal * c2 * distance;
            ball.transform.position = v1 + v2;

            Debug.Log(string.Format("v1:{0} ,v2:{1}",v1,v2));
            yield return null;
        }

    }

}
