using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventoryItemController : MonoBehaviour
{
    
    public string itemName;


    public Item item;

    public fishControler player;
    


    public int itemWorth;
    public TextMeshProUGUI itemWorthText;

    public Button RemoveButton;

    public void RemoveItem()
    {
        playerInventory.Instance.Remove(item);
        Destroy(gameObject);

        Debug.Log("removed item");
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemName = item.nameID;
        itemWorth = item.itemWorth;
        itemWorthText.text = item.itemWorth.ToString();
    }

    public void sellItem(){
        player.changeMoney(itemWorth);
        Debug.Log("added " + itemWorth.ToString() + " to the player");
        playerInventory.Instance.Remove(item);
    }

    public void itemClicked(){
        Debug.Log("item in inventory clicked");
        playerInventory.Instance.itemClicked(item);
    }

    private void Start()
    {
        player = GameObject.Find("Fisher").GetComponent<fishControler>();
        
    }

    public void getRod(){
        item = playerInventory.Instance.giveRod();
    }

    public void getHat(){
        item = playerInventory.Instance.giveRod();
    }
}
