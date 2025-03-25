using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalSquare : MonoBehaviour
{

    public float t;

    // Start is called before the first frame update
    public void Grow()
    {
        t = 0;
        if (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * t;
        }
    }
}