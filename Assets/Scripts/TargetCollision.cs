using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{

    public AnimationCurve curve;

    private Vector3 OScale;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        OScale = transform.localScale;
    }

    public void SoldClothing()
    {
        StartCoroutine(Selling());
    }

    public IEnumerator Selling()
    {
        OScale = transform.localScale;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = OScale * (1f - curve.Evaluate(t));
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = OScale * curve.Evaluate(t);
            yield return null;
        }
    }
}
