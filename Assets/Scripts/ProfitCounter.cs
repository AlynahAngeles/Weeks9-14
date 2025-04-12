using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProfitCounter : MonoBehaviour
{
    public TargetCollision linkedCloth;
    public static TMP_Text scoreDisplay;
    public static TMP_Text profitDisplay;
    public bool isBonus = false;

    public float itemPrice = 1.00f;

    public static float score = 0f;
    public static float profit = 0f;

    public static UnityEvent whenSell = new UnityEvent();

    void Start()
    {
        whenSell.AddListener(TrackSale);
        GetComponent<Button>().onClick.AddListener(Sold);

    }

    public void Sold()
    {
        if(linkedCloth == null)
        {
            Debug.Log("Linked cloth is missing!");
            return;
        }

        if (linkedCloth.isSelling)
        {
            Debug.Log("Still making a sale...");
            return;
        }

        if (!linkedCloth.gameObject.activeInHierarchy || linkedCloth.currentSales >= linkedCloth.maxSales)
        {
            Debug.Log("Item is sold out or inactive.");
            return;
        }

        linkedCloth.Sell();
        float addPoints = 1;

        if (isBonus)
        {
            addPoints = 2;
            Debug.Log("BONUS ACTIVE");
        }
                
        score += addPoints;
        profit += linkedCloth.itemPrice * addPoints;

        if(scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + score;
        }

        if(profitDisplay != null)
        {
            profitDisplay.text = "Money Earned: $" + profit + ".00";
        }

        whenSell.Invoke();
        
    }

    public void SetBonusActive(bool isActive) 
    { 
        isBonus = isActive;  // This should update the value of isBonus
        Debug.Log("isBonus is now: " + isBonus);
    }

    public void TrackSale()
    {
        Debug.Log("Sale event tracked");
    }
}
