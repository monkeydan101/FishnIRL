using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class fishControler : MonoBehaviour
{
    /*
     * 
     * this class controls the text appearing depending on the fish size, 
     * and with a wait time that depends on the size of the fish caught! 
     * 
     * 
     */
    private int waitTime; //the amount of time before the fish bites

    [SerializeField] public TMP_Text smallFishText;
    [SerializeField] public TMP_Text mediumFishText;
    [SerializeField] public TMP_Text bigFishText;

    [SerializeField] public Rigidbody fisherBody;

    [SerializeField] private Animator m_animator = null;

    
    [SerializeField] public itemList itemsListObj;

    



    //for keeping track of money!!!
    public int moneyCount = 0;
    [SerializeField] public TextMeshProUGUI moneyCountText;



    public bool fishOnTheLine;

    public string fishName;
    public bool touchingWater;

    public bool hookInWater;

    

    public List<Item> items = new List<Item>();




    //gear stats
    public int hatLuck;
    public int hatSkill;

    public int rodLuck;
    public int rodSkill;

    //hear models
    public GameObject currentHat = null;
    public GameObject fishingRod;
    
    public bool rodEquipt = false;

    // Start is called before the first frame update
    void Start()
    {
        moneyCountText.text = moneyCount.ToString();
        items = itemsListObj.returnItems().ToList(); //this constructs the complete list of items in the game

        smallFishText.enabled = false;
        mediumFishText.enabled = false;
        bigFishText.enabled = false;

        fishOnTheLine = false;
        hookInWater = false;

        fishingRod.SetActive(false);

        rodEquipt = false;

        



        //giving the player starter items:
        for(int i = 0; i < items.Count; i++) //this loop adds the right to the inventory
        {
            if(items[i].nameID == "starterRod")
            {
                playerInventory.Instance.getItem(items[i]); 
                //inventory.getItem(items[i]);
            }
        }
    }






    public void setFishName(string name) //takes info from pond to make the name of the fish known to this program
    {
        fishName = name;
    }

    public void setWaterStatus(bool result)
    {
        touchingWater = result;
    }




    void Update()
    {
        if (Input.GetKeyDown("e") && touchingWater && fishOnTheLine && hookInWater) //&& rod is equipt
        {
            Debug.Log("hook up");
            
            m_animator.SetTrigger("Land");
            
            hookInWater = false;
            
            
            m_animator.SetBool("hookInWater", hookInWater);
            
            
            smallFishText.enabled = false; //just to turn off whatever indicator that might have been on
            mediumFishText.enabled = false;
            bigFishText.enabled = false;

            if (fishOnTheLine) 
            {
                fishCaught();
            }
            else
            {
                fishNotCaught();
            }
            fishOnTheLine=false;
        }

        if (!touchingWater) //if the fisher has LEFT the water while a hook is in the water!
        {
            hookInWater=false;
            m_animator.SetBool("hookInWater", hookInWater);
            StopAllCoroutines();
            fishOnTheLine = false;
        }

        if (!hookInWater)
        {
            fishingRod.SetActive(false);
        }
        
        moneyCountText.text = moneyCount.ToString();
    }




    public void largeFish(string fishName)
    {
        if (touchingWater)
        {
            waitTime = Random.Range(5, 12);
            StartCoroutine(waiter(waitTime, bigFishText));

            Debug.Log("large fish function executed");
        }
        
    }
    public void medFish(string fishName)
    {
        if (touchingWater)
        {
            waitTime = Random.Range(4, 10);
            StartCoroutine(waiter(waitTime, mediumFishText));

            Debug.Log("medium fish function executed");
        }
        
    }
    public void smallFish(string fishName)
    {
        if (touchingWater)
        {
            waitTime = Random.Range(3, 8);
            StartCoroutine(waiter(waitTime, smallFishText));


            Debug.Log("small fish function executed");
        }
        
    }

    IEnumerator waiter(int waitTime, TMP_Text text) //this is for catching the fish, makes the marks appear after a random amount of time 
    {
        hookInWater = true;
        fishingRod.SetActive(true);
        m_animator.SetBool("hookInWater", hookInWater);
        if (touchingWater)
        {
            yield return new WaitForSeconds(waitTime);
            text.enabled = true;
            fishOnTheLine = true;
        }
      

        if (touchingWater)
        {
            StartCoroutine(fishOn(waitTime, text));
        }
        
        
    }

    IEnumerator fishOn(int waitTime, TMP_Text text)
    {
        int fishTimer = Random.Range(2, 4);
        yield return new WaitForSeconds(fishTimer);

        fishOnTheLine = false; //the fish is missed if not caught already
        text.enabled = false;
    }


    //these are to handle any time the user presses e!
    public void fishCaught()
    {
        Debug.Log(fishName + " caught!");

        for(int i = 0; i < items.Count; i++) //this loop adds the right to the inventory
        {
            if(items[i].nameID == fishName)
            {
                playerInventory.Instance.getItem(items[i]); 
                //inventory.getItem(items[i]);
            }
        }
    }
    public void fishNotCaught()
    {
        Debug.Log("You lost the fish...");
    }




//gear methods
    public void changeHat(Item newHat){
        hatLuck = newHat.hatLuck;
        hatSkill = newHat.hatSkill;

        currentHat = newHat.hatModel;
        fishingRod.SetActive(false);
    }

    public void changeRod(Item newRod){
        rodLuck = newRod.rodLuck;
        rodSkill = newRod.rodSkill;

        fishingRod = newRod.rodModel;
        fishingRod.SetActive(true);

        rodEquipt = true; //to make sure you can't fish without a rod equipt
    }


                 //for dequiping items!!
    public void noHat(){
        hatLuck = 0;
        hatSkill = 0;

        currentHat.SetActive(false);
    }

    public void noRod(){
        rodLuck = 0;
        rodSkill = 0;

        fishingRod.SetActive(false);
    }
                    //   !!!



    public int getSkillLvl(){            //this is to be used by the ponds to check if your skill level is high enough to fish in them
        return (rodSkill + hatSkill);
    }

    public int getLuckLvl(){
        return (rodLuck + hatLuck);
    }


//for moners
    public void changeMoney(int amount)
    {
        Debug.Log("item has been sold");
        moneyCount += amount;
    }
}
