using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] clothes; // Array of clothe prefabs to spawn
    public Vector3[] spawnPos; //Matching spawn positions

    void Start()
    {
        int count = Mathf.Min(clothes.Length, spawnPos.Length);

        for (int i = 0; i < count; i++)
        {
            if (clothes[i] != null)
            {
                Vector3 worldPosition = transform.position + spawnPos[i];
                Instantiate(clothes[i], worldPosition, Quaternion.identity);
            }
        }
    }
}

