using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class playerInventory : MonoBehaviour
{
    public static playerInventory Instance;
    public fishControler fisher;

    public Transform ItemContent;
    public Transform sellItemContent;


    //gear slot content
    public Transform hatSlot;
    public Transform rodSlot;

    public bool hatOn = false;
    public bool rodOn = false;

    public GameObject currentHat;
    public GameObject currentRod;

    public Item hatItem;
    public Item rodItem;


    public GameObject InventoryItem;

    public Toggle toggleRemove;

    public inventoryItemController[] inventoryItems;

    public List<Item> inventory = new List<Item>();
    // Start is called before the first frame update


    public Vector3 screenPosition;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        //Debug.Log(screenPosition.ToString());
    }
    




    
    public void Remove(Item item)
    {
        inventory.Remove(item);
    }



    public void getItem(Item item) //this adds the fish into the player inventory list!
    {
        inventory.Add(item);
        Debug.Log("Added one item to the list");
    }

    public List<Item> getInventory()
    {
        return inventory;
    }

    public void ListItems()
    {
        foreach(Transform item in ItemContent) //cleans up inventory so items dont multiply when this is called
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in rodSlot) //cleans up rodSlot so items dont multiply when this is called
        {
            Destroy(item.gameObject);
        }



        foreach (Item item in inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            var salePrice = obj.transform.Find("salePrice").GetComponent<TMP_Text>();

          

            itemName.text = item.nameID;
            itemIcon.sprite = item.icon;

            


            salePrice.gameObject.SetActive(false);

                                              
            if (toggleRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

            
        }


        //  ROD ZONE!!!
        if(currentRod != null)
        {

            currentRod = Instantiate(InventoryItem, rodSlot);
            var itemName = currentRod.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = currentRod.transform.Find("itemIcon").GetComponent<Image>();



            itemName.text = rodItem.nameID;
            itemIcon.sprite = rodItem.icon;
            
            StartCoroutine(waiterRodSlot());
        }
            




        //  !!!



        //  hAT ZONE!!!





        //  !!!




        StartCoroutine(waiter());
        Debug.Log("done waiting");

    }

    public void ListItemsForSale()
    {
        foreach(Transform item in sellItemContent) //cleans up inventory so items dont multiply when this is called
        {
            Destroy(item.gameObject);
        }




        foreach(Item item in inventory)
        {
            GameObject obj = Instantiate(InventoryItem, sellItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var salePrice = obj.transform.Find("salePrice").GetComponent<TMP_Text>();
            var saleButton = obj.transform.Find("saleButton").GetComponent<Button>();

            Debug.Log("Item name = ", itemName);

            itemName.text = item.nameID;
            itemIcon.sprite = item.icon;
            salePrice.text = item.itemWorth.ToString();

            salePrice.gameObject.SetActive(true);

            saleButton.gameObject.SetActive(true);
            

           
        }

        StartCoroutine(waiterSeller());
        Debug.Log("done waiting");
    }

    IEnumerator waiterRodSlot() //just a coroutine to ease the stress of the list items function, used for setting the itemData of the equipted rod
    {
        
        yield return new WaitForSeconds(0.1f);
        inventoryItemController controller = rodSlot.GetComponentInChildren<inventoryItemController>();
        controller.setItem(rodItem);

    }

    public void EnableItemsRemove()
    {
        Debug.Log("remove toggled");
        if (toggleRemove.isOn)
        {
            foreach (Transform item in ItemContent){
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }



    IEnumerator waiter()
{
    //Rotate 90 deg
    //Wait for 4 seconds
    yield return new WaitForSeconds(0.1f);
    SetInventoryItems();

}

IEnumerator waiterSeller()
{
    //Rotate 90 deg
    //Wait for 4 seconds
    yield return new WaitForSeconds(0.1f);
    SetInventoryItemsPostSaleMenu();

}





    public void SetInventoryItems()
    {
        //the problem is somewhere in this method, when its called its adding duplicate items
        //its doubling because the item stored in inventory is losing its reference to the item data
        
        inventoryItems = ItemContent.GetComponentsInChildren<inventoryItemController>();

        Debug.Log("current inventory manager array length: " + inventoryItems.Length.ToString());
        
        
        for(int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i].AddItem(inventory[i]);
            
        }

        
    }

    public void SetInventoryItemsPostSaleMenu()
    {
        

        inventoryItems = sellItemContent.GetComponentsInChildren<inventoryItemController>();
        
        Debug.Log("current inventory manager array length: " + inventoryItems.Length.ToString());
        for(int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i].AddItem(inventory[i]);
            
        }   

        
    }





    //TO DO: USE MOUSE POSITION TO DETERMINE IF THE ITEM CLICKED IS IN THE HAT/ROD AREA
    //       THEN CHECK IF A HAT/ROD IS EQUIPT, THEN FIX DEQUIPT!!!!! 
    public void itemClicked(Item item){ //for organizing gear interactions!
        if(rodItem != null)
        {
            inventoryItemController controller = rodSlot.GetComponentInChildren<inventoryItemController>();
            controller.setItem(rodItem);
            
        }
        if (item.isHat)
        {
            if((screenPosition.x > 600) && (screenPosition.x < 685) && (screenPosition.y > 230) && (screenPosition.y < 310)) //if hat is currently equipt and clicked --> dequipt!
            {




            }
            Debug.Log("Fishing hat clicked");



            if (hatOn == true)
            { //dequipt hat                           
                Debug.Log("Dequipting hat");
                dequiptHat(item);
                equiptHat(item);
            }
            else if (hatOn == false)
            { //equipt hat
                Debug.Log("Equiptiawng Hat");
                equiptHat(item);





            }

            inventory.Remove(item); //removes the item from the inventory list so when the inventory is generated the item wont be there



        }


        //COPY FOR ROD
        else if(item.isRod){

            if ((screenPosition.x > 600) && (screenPosition.x < 685) && (screenPosition.y > 115) && (screenPosition.y < 185)) //if rod is currently equipt and clicked --> dequipt!
            {
                Debug.Log("Dequipting rod, rod clicked!!");
                dequiptRod(item);
            }       
            
            else if(rodOn == true){ //dequipt rod                           
            Debug.Log("Dequipting rod");
                dequiptRod(item);
                equiptRod(item);


                inventory.Remove(item);
            }
            else if(rodOn == false){ //equipt rod
                Debug.Log("Equiptiawng Rod");
                equiptRod(item);



                inventory.Remove(item);

            }
            
        }

    }



    public void equiptHat(Item hat){                            //OLD CODE
        GameObject obj = Instantiate(InventoryItem, hatSlot);
        var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();

        currentHat = obj;

        Debug.Log("Item name = ", itemName);

        itemName.text = hat.nameID;
        itemIcon.sprite = hat.icon;
            
    
            
        fisher.changeHat(hat); //changes the players stats


        
        }
    

    public void dequiptHat(Item hat){    //OLD CODE
        if(hat != null){

            //BELOW IS TESTED CODE TO CLEAR THE RODSLOT

            


            getItem(hat); //adds the item to the inventory

            fisher.noHat();

           
        }
    }


    public void equiptRod(Item rod){                                                               
        Debug.Log("equipt called for rod");
        currentRod = Instantiate(InventoryItem, rodSlot);
        var itemName = currentRod.transform.Find("itemName").GetComponent<TMP_Text>();
        var itemIcon = currentRod.transform.Find("itemIcon").GetComponent<Image>();


        rodItem = rod;

        

        itemName.text = rod.nameID;
        itemIcon.sprite = rod.icon;
            
            
        fisher.changeRod(rod); //for changing stats
        Debug.Log("done equipting");


        //NEED TO ACCSESS THE CONTROLLER IN RODSLOT AND USE SETITEM ON IT TO MAKE IT REMEMBER THE ITEM DATA ITS HOLDING
        inventoryItemController controller = rodSlot.GetComponentInChildren<inventoryItemController>();
        controller.setItem(rod);




        //removes the item from the inventory after its moved into the rod slot/hat slot
        foreach (Transform itemObjects in ItemContent)
        {
            var itemNameObj = itemObjects.transform.Find("itemName").GetComponent<TMP_Text>();
            if (itemName.text == rod.nameID)
            {
                Destroy(itemObjects.gameObject);
                Debug.Log("found and destroyed");
            }
            break;
        }
    }


    public void dequiptRod(Item rod){

        Debug.Log("dequipt called for rod");
        if(rod != null){

            inventoryItemController controller = rodSlot.GetComponentInChildren<inventoryItemController>();
            controller.RemoveItem();

            getItem(rod); //adds the item to the inventory

            fisher.noRod();

            currentRod = null;
            rodOn = false;
            rodItem = null;

            ListItems();
          
        }
       
    }

    public Item giveRod(){
        return rodItem;
    }

    public Item giveHat(){
        return hatItem;
    }

}
