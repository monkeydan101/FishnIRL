using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    public string nameID;
    public Sprite icon;
    public int itemWorth; //for selling ofc


    //Gear Stats
    public bool isHat;
    public bool isRod;

    public int hatSkill; //decreases time to catch
    public int hatLuck; //increases chance for rare fish


    public int rodSkill;
    public int rodLuck; 

    public GameObject hatModel;
    public GameObject rodModel;

    public bool isEquipt;


    public Mesh mesh;
}
