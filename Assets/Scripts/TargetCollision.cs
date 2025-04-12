using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{

    public AnimationCurve curve;
    public float t;
    private Vector3 OScale;
    public bool isSelling = false;
    public float itemPrice = 1.00f;

    // Start is called before the first frame update
    void Start()
    {
        OScale = transform.localScale;
    }

    public void Sell()
    {
        if (!isSelling)
        { 
            StartCoroutine(Selling());
        }
    }
    private IEnumerator Selling()
    {
        isSelling = true;

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

        isSelling = false;
    }
}
