using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointerEvent1 : MonoBehaviour
{

    public Image cake;

    public Sprite highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MouseEnteredCupcake()
    {
        Debug.Log("Mouse is preparing to eat the cupcake!");
    }

    public void MouseExitedCupcake()
    {
        Debug.Log("Cupcake lives to see another day...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cake.sprite = highlight;
            Debug.Log("Oh my god, I'm a wizard!");

        }
    }
}
