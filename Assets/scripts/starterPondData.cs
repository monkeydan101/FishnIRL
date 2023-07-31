using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starterPondData : MonoBehaviour
{
    public int total;


    [SerializeField] public GameObject fisherObj;


    public bool hookInWater; //used to keep track of the hook status



    public int[] table = 
        { 50,//pond trout 
        20, //walleye
        15, //pickril
        10, //rainbow trout
        5 //snakehead
    };

    public string[] names =
    {
        "pond trout",
        "walleye",
        "pickerel",
        "rainbow trout",
        "snakehead"
    };

    private string newFish;
    public bool touchingFisher = false;

    private void OnTriggerEnter(Collider fisher)
    {
        if (fisher.gameObject.name == "Fisher")
            //AND if button pressed = "e"
        {
            touchingFisher = true;
            fisherObj.SendMessage("setWaterStatus", true);
        }
        
    }

    private void OnTriggerExit(Collider fisher)
    {
        if (fisher.gameObject.name == "Fisher")
        //AND if button pressed = "e"
        {
            touchingFisher = false;
            Debug.Log("left the water!");

            fisherObj.SendMessage("setWaterStatus", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hookInWater = false;

        foreach(var item in table)
        {
            total += item;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingFisher) //if fisher is in the water
        //AND if button pressed = "e"
        {
            if (Input.GetKeyDown("e") && !hookInWater)
            {
                newFish = grabFish(table, names);
                //if the fisher presses catch fast enough, he gets the fish! 
                //Debug.Log("caught fish = " + newFish);
                hookInWater = true;
            }

            else if (Input.GetKeyDown("e")) { //takin hook out of water
                hookInWater = false;
            }
        }
    }

    public string grabFish(int[] table, string[] names)
    {
        int randint;
        randint = Random.Range(0, total);

        int index = 0;



        while(randint >= table[index])
        {
            randint =- table[index];
            index++;
        }
        if (touchingFisher)
        {
            if (names[index] == "pond Trout" || names[index] == "pickrel") //small fish
            {
                fisherObj.SendMessage("smallFish", names[index]);
            }
            if (names[index] == "walleye" || names[index] == "rainbow trout") //small fish
            {
                fisherObj.SendMessage("medFish", names[index]);
            }
            if (names[index] == "snakehead") //large fish
            {
                fisherObj.SendMessage("largeFish", names[index]);
            }

            fisherObj.SendMessage("setFishName", names[index]); //makes the name of the fish known to the fisher game object program!
            return names[index];
        }

        return names[index];

    }
    
}
