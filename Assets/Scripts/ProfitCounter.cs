using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProfitCounter : MonoBehaviour
{
    public TargetCollision linkedCloth; //Attach to the TargetCollision script to reference it in code later
    public static TMP_Text scoreDisplay; //public declaration for the score counter
    public static TMP_Text profitDisplay; //public declaration for the profit counter
    public bool isBonus = false; //set the bonus bool to false by default 

    public float itemPrice = 1.00f; //public float to assign price values to the clothing pieces

    public static float score = 0f; //set the score to 0 by default
    public static float profit = 0f; //set the profit to 0 by default

    public static UnityEvent whenSell = new UnityEvent(); //When the whenSell happens, run the UnityEvent

    void Start()
    {
        whenSell.AddListener(TrackSale); //subscribe the TrackSale to whenSell, run TrackSale when whenSell runs
        GetComponent<Button>().onClick.AddListener(Sold); //When the button is clicked, add a listener Sold, so it runs when an item is clicked

    }

    public void Sold() //Initiate the function Sold
    {
        if(linkedCloth == null) //If the linkedCloth is not assigned
        {
            Debug.Log("Linked cloth is missing!"); //debug to check if the prefabs were connected properly
            return;
        }

        if (linkedCloth.isSelling) //if the item is still running its coroutine
        {
            Debug.Log("Still making a sale..."); //Return this debug and don't add a point
            return;
        }

        if (!linkedCloth.gameObject.activeInHierarchy || linkedCloth.currentSales >= linkedCloth.maxSales) //If the clothing is not active in the hierarchy or the current sales are moer than the max, run the code
        {
            Debug.Log("Item is sold out or inactive."); //Tell the player the item is sold out
            return;
        }

        linkedCloth.Sell(); //When the Sell() function runs in the TargetCollision script
        float addPoints = 1; //Add one point to the score

        if (isBonus) //If the bonus is activated
        {
            addPoints = 2; //add 2 points
            Debug.Log("BONUS ACTIVE"); //debug log to check if the bonus was running properly
        }
                
        score += addPoints; //Increment the score based on the value of addPoints
        profit += linkedCloth.itemPrice * addPoints; //Increment the profit based on the sold item's assigned price * the amount by if the bonus is on or not (*1 for no bonus, *2 for bonus)

        if(scoreDisplay != null) //if the scoreDisplay is not assigned
        {
            scoreDisplay.text = "Score: " + score; //Assign it this string and display it for the player
        }

        if(profitDisplay != null) //if the profitDisplay is not assigned
        {
            profitDisplay.text = "Money Earned: $" + profit + ".00"; //Assign it this string and display it for the player
        }

        whenSell.Invoke(); //Invoke each sale to be able to count and run the bonus burst accordingly
        
    }

    public void SetBonusActive(bool isActive)  //if the bonus is ACTIVE, run this function
    { 
        isBonus = isActive;  //Update the value of isBonus to TRUE

        if(linkedCloth != null) //if the linkedCloth is not assigned, run the code
        {
            if (isBonus) //If the bonus is on
            {
                linkedCloth.StartBonusEffect(); //Start the bonus pulse effect
            }
            else //in any other case
            {
                linkedCloth.StopBonusEffect(); //Stop the pulse effect
            }
        }
    }

    public void TrackSale() //TrackSale function
    {
        Debug.Log("Sale event tracked"); //Debug line to check if the bonus is starting when it's supposed to
    }
}
