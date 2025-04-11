using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] clothes; // Array of clothes prefabs to spawn
    public Vector3[] spawnPos; //Matching spawn positions
    public GameObject buttonPrefab;
    public Transform buttonParent;

    void Start()
    {
        Debug.Log("Spawner running");

        int count = Mathf.Min(clothes.Length, spawnPos.Length);

        for (int i = 0; i < count; i++)
        {
            GameObject spawnedCloth = Instantiate(clothes[i], spawnPos[i], Quaternion.identity);
            GameObject button = Instantiate(buttonPrefab, buttonParent);

            var connector = button.GetComponent<ProfitCounter>();
            connector.linkedCloth = spawnedCloth.GetComponent<TargetCollision>();

        }
    }
}

