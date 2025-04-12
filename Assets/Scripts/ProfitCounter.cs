using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfitCounter : MonoBehaviour
{
    public TargetCollision linkedCloth;
    public static TMP_Text scoreDisplay;

    private static int score = 0;

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
            if (scoreDisplay != null)
            {
                scoreDisplay.text = "Score: " + score.ToString();
            }
        }
    }
}
