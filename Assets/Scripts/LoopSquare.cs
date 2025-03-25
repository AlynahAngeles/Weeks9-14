using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSquare : MonoBehaviour
{

    public float t;

    public void Grow()
    {
        StartCoroutine(GetBigger());
    }

    // Start is called before the first frame update
    IEnumerator GetBigger()
    {
        t = 0;
        //Debug.Log("Starting!");
        while (t < 1) //ALWAYS use a while statement with Coroutines (NOT if statements), if statements will only happen once while a while statement will be able to break up your code with Coroutines!
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * t;
            //Debug.Log("Time to yield!");
            yield return null;
        }
        //Debug.Log("The End");
    }
}
