using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerInventory : MonoBehaviour
{
    public static playerInventory Instance;
    public GameObject fisher;

    public Transform ItemContent;
    public Transform sellItemContent;

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


        foreach(Item item in inventory)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").gameObject;
            var saleButton = obj.transform.Find("saleButton").gameObject;

            Debug.Log("Item name = ", itemName);

            itemName.text = item.nameID;
            itemIcon.sprite = item.icon;
            
            saleButton.SetActive(false);


                                              
            if (toggleRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

            
        }

        SetInventoryItems();
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
            var itemWorth = obj.transform.Find("salePrice").gameObject;
            var saleButton = obj.transform.Find("saleButton").gameObject;

            Debug.Log("Item name = ", itemName);

            itemName.text = item.nameID;
            itemIcon.sprite = item.icon;

            saleButton.SetActive(true);
            itemWorth.SetActive(true);

           
        }

        SetInventoryItemsPostSaleMenu();
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

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<inventoryItemController>();
        
        for(int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i].AddItem(inventory[i]);
        }

        
    }

    public void SetInventoryItemsPostSaleMenu()
    {
        inventoryItems = sellItemContent.GetComponentsInChildren<inventoryItemController>();
        
        for(int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i].AddItem(inventory[i]);
        }

        
    }

}
