using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusBurst : MonoBehaviour
{

    private int sellCount = 0;
    private float sellTime = 3f;
    private Coroutine reset;
    public Coroutine bonusCoroutine;
    private bool activeBonus = false;

    void Start()
    {
        ProfitCounter.whenSell.AddListener(whenSell);
        Debug.Log("BonusBurst subscribed to whenSell.");

    }

    void whenSell()
    {
        Debug.Log("Sale detected! Sell count: " + sellCount);
        sellCount++;

        if (sellCount == 1)
        {
            reset = StartCoroutine(ResetCounter());
        }

        if (sellCount >= 5 && !activeBonus)
        {
            ActivateBonus();
        }
    }

    public IEnumerator ResetCounter()
    {
        yield return new WaitForSeconds(sellTime);
        sellCount = 0;
    }

    public void ActivateBonus()
    {
        if (activeBonus)
        {
            Debug.Log("Bonus already activated.");
            return;
        }

        Debug.Log("BONUS BURST!");
        activeBonus = true;

        ProfitCounter[] allCounters = FindObjectsOfType<ProfitCounter>();

        for (int i = 0; i < allCounters.Length; i++)
        {
            Debug.Log("Activating bonus for: " + allCounters[i].gameObject.name);
            allCounters[i].SetBonusActive(true);
        }

        if(bonusCoroutine != null)
        {
            StopCoroutine(bonusCoroutine);
        }

        bonusCoroutine = StartCoroutine(EndBurst());

    }
    public IEnumerator EndBurst()
    {
        yield return new WaitForSeconds(10f);

        ProfitCounter[] allCounters = FindObjectsOfType<ProfitCounter>();

        for (int i = 0; i < allCounters.Length; i++)
        {
            allCounters[i].SetBonusActive(false);
        }

        activeBonus = false;
        sellCount = 0;
        reset = null;
        bonusCoroutine = null;
        Debug.Log("Bonus Burst has ended.");
    }

    private void OnDestroy()
    {
        ProfitCounter.whenSell.RemoveListener(whenSell);
    }
}
