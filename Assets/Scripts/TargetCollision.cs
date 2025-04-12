using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{

    public AnimationCurve curve;
    public float t;
    private Vector3 OScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Sell()
    {
        StartCoroutine(Selling());
    }
    private IEnumerator Selling()
    {
        OScale = transform.localScale;

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.localScale = OScale * (1f - curve.Evaluate(t));
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = OScale * curve.Evaluate(t);
            yield return null;
        }
    }
}
