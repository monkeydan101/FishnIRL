using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        
    }
    





    //TO-DO: 
    //to fix the error with the item losing its reference to the item that is equipt:
    //         - reference the inventoryItemController component in hetslot/rodslot in player inventory, set the item inside the controller using the 2 newest methods
    //         - 






    
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
        //foreach(inventoryItemController item in inventoryItems){
        //    Destroy(item.gameObject);
        //}


        foreach(Item item in inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            var salePrice = obj.transform.Find("salePrice").GetComponent<TMP_Text>();

            Debug.Log("Item name = ", itemName);

            itemName.text = item.nameID;
            itemIcon.sprite = item.icon;
            
            
            salePrice.gameObject.SetActive(false);

                                              
            if (toggleRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

            
        }
       
        StartCoroutine(waiter());
        Debug.Log("done waiting");


        
    }

/*
    public void cleanControllerList(){
        inventoryItems = null;
    }
*/
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

        




        /*
        //NEW PROPOSITION: clear the inventorycontroller array, then fill it using the items that are in item content, ASSIGN THEM the relevant item scriptableObjects

        Array.Clear(inventoryItems, 0, inventoryItems.Length);

        for(int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i] = inventory[i].gameObject.GetComponent<inventoryItemController>();
        }
        */
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



    public void itemClicked(Item item){ //for organizing gear interactions!
        if(item.isHat){
            if(item.isEquipt){ //dequipt hat
                dequiptHat(item);
            }
            else if(hatOn == false){ //equipt hat
                StartCoroutine(waiterEquiptHat(item));
                
                inventoryItemController controller = hatSlot.GetComponent<inventoryItemController>();
                controller.RemoveItem();

                
            }
        }

       
        //COPY FOR ROD
        else if(item.isRod){
            if(item.isEquipt){ //dequipt hat
                dequiptRod(item);
            }
            else if(rodOn == false){ //equipt hat
                StartCoroutine(waiterEquiptRod(item));
                
                inventoryItemController controller = rodSlot.GetComponent<inventoryItemController>();
                controller.RemoveItem();
            }
        }

    }

    public IEnumerator waiterEquiptHat(Item item)
{
    //Rotate 90 deg
    //Wait for 4 seconds
    yield return new WaitForSeconds(0.1f);
    equiptHat(item);

}

public IEnumerator waiterEquiptRod(Item item)
{
    //Rotate 90 deg
    //Wait for 4 seconds
    yield return new WaitForSeconds(0.1f);
    equiptRod(item);

}


    public void equiptHat(Item hat){
        GameObject obj = Instantiate(InventoryItem, hatSlot);
        var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();

        currentHat = obj;

        Debug.Log("Item name = ", itemName);

        itemName.text = hat.nameID;
        itemIcon.sprite = hat.icon;
            
    
            
        fisher.changeHat(hat); //changes the players stats
        hat.isEquipt = true; //this tracks if the item is equipt so it may be removed later on
        }
    

    public void dequiptHat(Item hat){
        Destroy(currentHat);//removes item from hatslot
        getItem(hat); //adds the item to the inventory

        fisher.noHat();
    }


    public void equiptRod(Item rod){
        GameObject obj = Instantiate(InventoryItem, rodSlot);
        var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();

        currentRod = obj;

        Debug.Log("Item name = ", itemName);

        itemName.text = rod.nameID;
        itemIcon.sprite = rod.icon;
            
            
        fisher.changeRod(rod);
        rod.isEquipt = true; 
    }


    public void dequiptRod(Item rod){
        Destroy(currentRod);//removes item from rodslot
        getItem(rod); //adds the item to the inventory

        fisher.noRod();
    }

    public Item giveRod(){
        return rodItem;
    }

    public Item giveHat(){
        return hatItem;
    }

}
