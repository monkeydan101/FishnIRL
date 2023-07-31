using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventoryItemController : MonoBehaviour
{
    // Start is called before the first frame update
    Item item;

    public int itemWorth = 0;
    [SerializeField] public TextMeshProUGUI itemWorthText;

    public Button RemoveButton;
    public void RemoveItem()
    {
        playerInventory.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemWorth = item.itemWorth;
        itemWorthText.text = itemWorth.ToString();
    }

    private void Start()
    {
        itemWorthText.gameObject.SetActive(false);
    }
}
