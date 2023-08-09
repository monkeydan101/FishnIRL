using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemList : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    [SerializeField] public Item pondTrout;
    [SerializeField] public Item rainbowTrout;
    [SerializeField] public Item pickrel;
    [SerializeField] public Item snakehead;
    [SerializeField] public Item walleye;

    //GEAR ITEMS

    [SerializeField] public Item starterRod;

    // Start is called before the first frame update
    void Start()
    {
        items.Add(pondTrout);
        items.Add(rainbowTrout);
        items.Add(walleye);
        items.Add(snakehead);
        items.Add(pickrel);

        //GEAR

        
        items.Add(starterRod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Item> returnItems()
    {
        return items;
    } 
}
