using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rodSystem : MonoBehaviour
{
    public Item currentRod;
    [SerializeField] private MeshFilter modelToChange;
    

    void Start()
    {
        currentRod = null;
}

    
    public void changeRod(Item newItem)
    {
        currentRod = newItem;

        modelToChange.mesh = currentRod.mesh;

        
    }
}
