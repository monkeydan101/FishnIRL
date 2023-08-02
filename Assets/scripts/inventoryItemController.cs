using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventoryItemController : MonoBehaviour
{







    //THE PROBLEM:
    //THE ITEMS ADDED LOSE THEIR REFERENCE TO THEIR ITEM DATA ONCE AN ITEM IS SOLD, I DONT KNOW WHY











    // Start is called before the first frame update
    public Item item;

    public fishControler player;
    


    public int itemWorth = 0;
    public TextMeshProUGUI itemWorthText;

    public Button RemoveButton;
    public void RemoveItem()
    {
        playerInventory.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemWorthText.text = item.itemWorth.ToString();
    }

    public void sellItem(){
        player.changeMoney(itemWorth);
    }

    private void Start()
    {
        player = GameObject.Find("Fisher").GetComponent<fishControler>();
        
    }
}
