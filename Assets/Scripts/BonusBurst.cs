using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusBurst : MonoBehaviour
{

    public int sellCount = 0; //State an int to count the sales, assign it to 0 by default
    public float sellTime = 3f; //Set a float for a counter of when to initiate the burst
    public Coroutine reset; //State a coroutine to reset the burst
    public Coroutine bonusCoroutine; //State a coroutine FOR the bonus burst
    private bool activeBonus = false;//State a bool for the bonus burst

    void Start()
    {
        ProfitCounter.whenSell.AddListener(whenSell); //Add a listener so that the program knows when to initiate the bonus burst 
        //Bonus Burst is now subscribed to ProfitCounter
    }

    void whenSell() //start function whenSell
    {

        if (activeBonus) //If the bonus is active
            return;

        sellCount++; //Increment the sellCount  

    
        if (sellCount == 1) //if the sellCount is 1, run the code
        {
            reset = StartCoroutine(ResetCounter()); //Run the coroutine ResetCounter to run the counter again
        }

        if (sellCount >= 5 && !activeBonus) //if the sell count is greater or equal to 5, the bonus burst requirements have been met
        {
            ActivateBonus(); //activate the bonus burst
        }
    }

    public IEnumerator ResetCounter() //Coroutine to reset the counter
    {
        yield return new WaitForSeconds(sellTime); //Wait for 3 seconds before restarting another timer
        sellCount = 0; //reset the sell count to 0 if the requirements were not met
    }

    public void ActivateBonus() //function ActivateBonus (runs when the burst is activated)
    {
        if (activeBonus) //If the bonus is active
        {
            Debug.Log("Bonus already activated."); //Debug log to check if code was running properly
            return;
        }

        Debug.Log("BONUS BURST!"); //Debug log to check if burst was initiating at the right time
        activeBonus = true; //Set activeBonus to true

        ProfitCounter[] allCounters = FindObjectsOfType<ProfitCounter>(); 

        for (int i = 0; i < allCounters.Length; i++)
        {
            Debug.Log("Activating bonus for: " + allCounters[i].gameObject.name); //Find all objects that use the counter script
            allCounters[i].SetBonusActive(true); //Set the bonus effects to true (makes the score + profit double for the duration of the burst)
        }

        if(bonusCoroutine != null) //if the bonus coroutine is not null, run the code
        {
            StopCoroutine(bonusCoroutine); //stop the bonusCoroutine
        }

        bonusCoroutine = StartCoroutine(EndBurst()); //This initiates the process of ending the burst

    }
    public IEnumerator EndBurst() //the process of ending a burst
    {
        yield return new WaitForSeconds(10f); //Wait for 10 seconds before ending the burst

        ProfitCounter[] allCounters = FindObjectsOfType<ProfitCounter>(); //Find all the objects that use the counter script 

        for (int i = 0; i < allCounters.Length; i++) //For each sale counter that was found above
        {
            allCounters[i].SetBonusActive(false); //set the bonus effects to false (turns the score and profit value back to default)
        }

        activeBonus = false; //set the activeBonus bool to false
        sellCount = 0; //Reset the sellCount to 0 so it can count for another burst
        reset = null; //null the reset so that it can be initiated again
        bonusCoroutine = null; //null the bonusCoroutine as well so that it resets 
        Debug.Log("Bonus Burst has ended."); //Debug log to make sure everything was working as intended
    }

    private void OnDestroy() //When destroying a prefab (out of inventory items)
    {
        ProfitCounter.whenSell.RemoveListener(whenSell); //remove the listener/subscriber
    }
}
