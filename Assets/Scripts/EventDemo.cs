using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent OnTimerHasFinished;
    public float TimerLength = 3f;
    public float t;
    public void MouseJustEnteredImage()
    {
        Debug.Log("The mouse just entered the image!");
        banana.localScale = Vector3.one * 1.2f;
    }

    public void MouseJustExitedImage()
    {
        Debug.Log("The mouse just exited the image!");
        banana.localScale = Vector3.one;
    }


    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > TimerLength)
        {
            OnTimerHasFinished.Invoke();
            t = 0;
        }
    }
}
