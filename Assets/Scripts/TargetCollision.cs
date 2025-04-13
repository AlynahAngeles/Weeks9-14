using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{

    public AnimationCurve curve; //Stating an animation curve for the clothing lerp animation
    public float t; //setting a float t for the animation curve
    private Vector3 OScale; //Making a vector 3 for the transform
    public bool isSelling = false; //Creating a bool to track when making a sale or not 
    public float itemPrice = 1.00f; //Creating a float for the item pricing

    public float maxSales; //Creating a float to set a randomized variable for the max amount of clothes in inventory
    public float currentSales = 0; //create a float for the current sales by default (0)

    public TMP_Text soldOutText; //Calling the TMP_Text from the UI so I can turn it on and off through code
    public Coroutine sellingCoroutine; //Creating a coroutine for actually selling the products
    public Coroutine bonusPulse; //Creating a coroutine for a special effect for stronger signifiers when player enables burst

    // Start is called before the first frame update
    void Start()
    {
        OScale = transform.localScale; //transform the scale of the prefabs according to the animation curve
    }

    public void Sell() //initiating the function "Sell"
    {
        if(isSelling && currentSales >= maxSales) //If an item is getting sold & max inventory is still not 0, run this code
        {
            return; //Run the function again
        }

        currentSales++; //Increment the score by 1 whenever an item is sold
        sellingCoroutine = StartCoroutine(Selling()); //Run the Coroutine "Selling" when an item is sold

        if (currentSales >= maxSales) //if the current sales are greater than the maxSales (or if maxSales has gone down to 0), run this code
        {
            StopAllCoroutines(); //End all coroutines for the item that is sold out 
            gameObject.SetActive(false); //Disable the prefab in the Hierarchy

            if(soldOutText != null) //If the Sold Out text does not exist in the screen
            {
                soldOutText.text = "SOLD OUT!"; //Set the text box to say "SOLD OUT!"
                soldOutText.gameObject.SetActive(true); //Set the Sold Out UI to true (activate so it shows up in the scene)
                 
                soldOutText.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up); //Transform it's position so that it places right where the item is placed
            }
        }

        sellingCoroutine = StartCoroutine(Selling()); //Start the Coroutine "Selling"
            
    }
    public IEnumerator Selling() //Initiating the Coroutine "Selling"
    {
        isSelling = true; //If an item is being sold, adjust the bool isSelling to "true"

        t = 0f; //set the value of t = 0
        while (t < 1f) //While t is less than 1, run the code
        {
            t += Time.deltaTime; //setting t to be the value of time passed
            transform.localScale = OScale * (1f - curve.Evaluate(t)); //transforming the scale of the prefab to shrink based on the AnimationCurve, animate relative to time (t)
            yield return null; //animates gradually, so that the motion is smooth
        }

        yield return new WaitForSeconds(Random.Range(1f, 10f)); //Wait for a random amount of seconds before growing back to original size

        t = 0f; //Reset the value of t = 0;
        while (t < 1f) //while t is less than 1
        {
            t += Time.deltaTime; //setting t to be the value of time passed
            transform.localScale = OScale * curve.Evaluate(t); //transforming the scale of the prefab and making it grow based on the Animation curve, animate relative to time (t)
            yield return null; //animates gradually, so that the motion is smooth
        }

        isSelling = false; //Turn the isSelling bool to false, which makes it clickable again
    }

    public IEnumerator PulseEffect() //Inititating Pulse Effect
    {
        float pulseSpeed = 5f; //setting the pulse speed to 5f (rapid pulsing)
        float pulseAmount = 0.3f; //grow the prefab by 30%

        while (true) //While the player is in burst mode, run this code
        {
            float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount; //using Mathf.sin, animate the scale based on the function sin (going in between -1, 1) and scale it based on the time, speed of the pulse and the size transform (30%/.3)
            transform.localScale = OScale * scale; //Transform the local scale from it's original size and multiply it by the values that come from the line above; this makes the prefabs grow and shrink
            yield return null; //yield animation per frame (don't run every transform all at once)
        }
    }

    public void StartBonusEffect() //What runs when the player is in burst mode
    {
        if(bonusPulse == null) //if the bonus pulse isn't running, run the code below
        {
            bonusPulse = StartCoroutine(PulseEffect()); //Run the coroutine for the pulse effect
        }
    }

    public void StopBonusEffect() //to stop the pulse 
    {
        if(bonusPulse != null) //if the bonus pulse IS running, run the code below
        {
            StopCoroutine(bonusPulse); //stop the Coroutine
            bonusPulse = null; //make the pulse null (so it restarts in case the player triggers it again
            transform.localScale = OScale; //set the scale of the prefabs back to its original scale
        }
    }
}
