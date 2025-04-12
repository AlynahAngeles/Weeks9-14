using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] clothes; // Array of clothes prefabs to spawn
    public Vector3[] spawnPos; //Matching spawn positions
    public Vector2[] buttonPosition;
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public TMP_Text scoreDisplay;
    public TMP_Text profitDisplay;

    public BonusBurst burstScript;
    public ProfitCounter counterScript;
    public GameObject SoldOutSign;
    public Canvas main;

    void Start()
    {
        Debug.Log("Making big bucks today!");

        if(scoreDisplay == null)
        {
            scoreDisplay = GameObject.Find("Score")?.GetComponent<TMP_Text>();
        }

        if(profitDisplay == null)
        {
            profitDisplay = GameObject.Find("Profit")?.GetComponent<TMP_Text>();
        }

        ProfitCounter.scoreDisplay = scoreDisplay;
        ProfitCounter.profitDisplay = profitDisplay;

        int count = Mathf.Min(clothes.Length, spawnPos.Length);

        for (int i = 0; i < count; i++)
        {

            GameObject spawnedCloth = Instantiate(clothes[i], spawnPos[i], Quaternion.identity);
            TargetCollision target = spawnedCloth.GetComponent<TargetCollision>();

            GameObject soldText = Instantiate(SoldOutSign, main.transform);
            soldText.SetActive(false);
            target.soldOutText = soldText.GetComponent<TMP_Text>();

            if(target != null)
            {
                target.maxSales = Random.Range(1,5);
            }

            // Spawn UI Button
            GameObject spawnedButton = Instantiate(buttonPrefab, buttonParent);
            ProfitCounter counter = spawnedButton.GetComponent<ProfitCounter>();

            if (counter != null && target != null)
            {
                counter.linkedCloth = target;
            }

            RectTransform button = spawnedButton.GetComponent<RectTransform>();

            if (button != null)
            {
                button.anchoredPosition = buttonPosition[i];
            }
        }

        if(burstScript == null)
        {
            burstScript = FindObjectOfType<BonusBurst>();
        }

        if(counterScript == null)
        {
            counterScript = FindObjectOfType<ProfitCounter>();
        }
    }
}

