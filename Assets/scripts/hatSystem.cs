using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatSystem : MonoBehaviour
{
    public Item currentHat;
    [SerializeField] private MeshFilter modelToChange;
    public GameObject hatObject;

    void Start()
    {
        currentHat = null;
    }


    public void changeHat(Item newItem) //use with a null item to "dequipt" the hat (makes the object invisible)
    {
        if(newItem != null)
        {
            hatObject.SetActive(true);
            currentHat = newItem;

            modelToChange.mesh = currentHat.mesh;
        }

        else //for dequipt
        {
            currentHat = null;
            hatObject.SetActive(false);
        }


    }
}
