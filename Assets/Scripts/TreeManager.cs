using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
   public CoroutineGrower[] trees;

   public void StartGrowingtrees()
    {
        //foreach(CoroutineGrower coroutineGrower in trees)
        //{
        //    StartCoroutine(coroutineGrower.Grow());
        //}
        StartCoroutine(GrowAllTheTrees());
    }

    IEnumerator GrowAllTheTrees()
    {
        Debug.Log("Waiting for Tree 0");
        yield return StartCoroutine(trees[0].Grow());
        Debug.Log("Waiting for Tree 1");
        yield return StartCoroutine(trees[1].Grow());
        Debug.Log("Waiting for Tree 2");
        yield return StartCoroutine(trees[2].Grow());
        Debug.Log("Finishing!");

    }
}
