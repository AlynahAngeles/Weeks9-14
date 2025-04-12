using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfitCounter : MonoBehaviour
{
    public TargetCollision linkedCloth;
    public static TMP_Text scoreDisplay;
    public static TMP_Text profitDisplay;

    public float itemPrice = 1.00f;

    private static int score = 0;
    private static float profit = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Sold);
    }

    void Sold()
    {
        if (linkedCloth != null && !linkedCloth.isSelling)
        {
            linkedCloth.Sell();
            score++;

            profit += linkedCloth.itemPrice;

            if (scoreDisplay != null)
            {
                scoreDisplay.text = "Score: " + score;
            }

            if (profitDisplay != null)
            {
                profitDisplay.text = "Money Earned: $" + profit + ".00";
            }
        }
    }
}
