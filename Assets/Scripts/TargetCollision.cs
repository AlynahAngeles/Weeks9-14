using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{

    public AnimationCurve curve;
    public float t;
    private Vector3 OScale;
    public bool isSelling = false;
    public float itemPrice = 1.00f;

    public ProfitCounter profitCounter;
    public float maxSales = 100;
    public float currentSales = 0;

    public TMP_Text soldOutText;

    private Coroutine sellingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        OScale = transform.localScale;
    }

    public void Sell()
    {
        if(isSelling || currentSales >= maxSales)
        {
            Debug.Log("Selling in progress or item sold out.");
            return;
        }

        currentSales++;
        sellingCoroutine = StartCoroutine(Selling());

        if (currentSales >= maxSales)
        {
            StopAllCoroutines();
            gameObject.SetActive(false);

            if(soldOutText != null)
            {
                soldOutText.text = "SOLD OUT!";
                soldOutText.gameObject.SetActive(true);

                soldOutText.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            }

            Debug.Log("Item is sold out!");
        }

        sellingCoroutine = StartCoroutine(Selling());
            
    }
    public IEnumerator Selling()
    {
        isSelling = true;

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.localScale = OScale * (1f - curve.Evaluate(t));
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 3f));

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
