using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] clothes; // Array of clothes prefabs to spawn
    public Vector3[] spawnPos; //Matching spawn positions
    public Vector3 buttonPos;
    public GameObject buttonPrefab;
    public Transform buttonParent;

    void Start()
    {
        Debug.Log("Restocking...");

        int count = Mathf.Min(clothes.Length, spawnPos.Length);

        for (int i = 0; i < count; i++)
        {

            GameObject spawnedCloth = Instantiate(clothes[i], spawnPos[i], Quaternion.identity);
            TargetCollision target = spawnedCloth.GetComponent<TargetCollision>();

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
                spawnedButton.GetComponent<RectTransform>().anchoredPosition = buttonPos[i];
            }
        }
    }
}

