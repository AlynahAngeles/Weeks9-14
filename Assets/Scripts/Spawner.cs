using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] clothes; // Array of clothes prefabs to spawn
    public Vector3[] spawnPos; //Array of spawn positions FOR the prefabs 
    public Vector2[] buttonPosition; //array of button positions for each clothing item
    public GameObject buttonPrefab; //to be able to call the button prefab in code (can't assign through hierarchy because instantiated at runtime)
    public Transform buttonParent; //An empty gameObject so that I can place the buttons according to where its parent is (made it easier to find coords for each button)
    public TMP_Text scoreDisplay; //Calling the UI to display the player's score
    public TMP_Text profitDisplay; //Calling the UI to display the player's money earned

    public BonusBurst burstScript; //Stating the class to call the BonusBurst script
    public ProfitCounter counterScript; //Stating the class to call the ProfitCounter script
    public GameObject SoldOutSign; //Stating a gameObject so I can call it in the code--this is for the SOLD OUT sign that appears when a player reaches max sales
    public Canvas main; //Stating the main Canvas from the hierarchy so i can call it in the code

    void Start()
    {
        Debug.Log("Making big bucks today!"); //Debug Log to see if code was running properly

        if(scoreDisplay == null) //If the score display does not exist, run this code
        {
            scoreDisplay = GameObject.Find("Score")?.GetComponent<TMP_Text>(); //Find the gameObject called "Score", and display on the screen a component of the TMP_Text class
        }

        if(profitDisplay == null) //If the score display does not exist, run this code
        {
            profitDisplay = GameObject.Find("Profit")?.GetComponent<TMP_Text>(); //Find the gameObject called "Profit", and display on the screen a component of the TMP_Text class
        }

        ProfitCounter.scoreDisplay = scoreDisplay; //Stating the scoreDisplay to equal the scoreDisplay from the script "scoreCounter"
        ProfitCounter.profitDisplay = profitDisplay; //Stating the profitDisplay to equal the scoreDisplay from the script "ProfitCounter"

        int count = Mathf.Min(clothes.Length, spawnPos.Length); //makes sure that when the program instantiates everything, that elements align to their rightful spot (e.g. 1-1, 2-2, etc.)

        for (int i = 0; i < count; i++) //Run this code as many times as there are clothes and positions 
        {

            GameObject spawnedCloth = Instantiate(clothes[i], spawnPos[i], Quaternion.identity); //Instantiate the clothes at their rightful spawn point based on count number
            TargetCollision target = spawnedCloth.GetComponent<TargetCollision>(); //Attach the Target collision for each spawned piece of clothing

            GameObject soldText = Instantiate(SoldOutSign, main.transform); //Instantiate the SOLD OUT text
            soldText.SetActive(false); //Make it hidden for now while there are still clothes on the rack
            target.soldOutText = soldText.GetComponent<TMP_Text>(); //Get the text component of the soldText and assign it to the target class; this makes it so when a piece of clothing is sold out, the prefab is replaced with the "SOLD OUT" TMP

            if(target != null) //if target is not null, run this code
            {
                target.maxSales = Random.Range(25, 250); //Set the max sales (inventory) and assign it a random number between 50 and 500
            }

            GameObject spawnedButton = Instantiate(buttonPrefab, buttonParent); //Instantiate the button prefab and its parent
            ProfitCounter counter = spawnedButton.GetComponent<ProfitCounter>(); //Attach this button to the ProfitCounter script

            if (counter != null && target != null) //If the counter AND target is not null, run this loop 
            {
                counter.linkedCloth = target; //Connects the button to the right piece of clothing so when the target collision is hit, the right clothing piece runs its coroutines
            }

            RectTransform button = spawnedButton.GetComponent<RectTransform>(); //Get the button's transform to place on screen

            if (button != null) //if button is not null, run the code
            {
                button.anchoredPosition = buttonPosition[i]; //Place the button at its proper anchored position using the Array of assigned positions
            }
        }

        if(burstScript == null) //if the burstScript is not assigned,
        {
            burstScript = FindObjectOfType<BonusBurst>(); //Assign it to the Bonus Burst script
        }

        if(counterScript == null)  //if the counterScript is not assigned,
        {
            counterScript = FindObjectOfType<ProfitCounter>(); //Assign it to the ProfitCounter script
        }
    }
}

