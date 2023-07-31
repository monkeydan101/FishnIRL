using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atm : MonoBehaviour
{
    public GameObject ATM_UI;

    // Start is called before the first frame update
    void Start()
    {
        ATM_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
       Debug.Log("collision with ATM");
        ATM_UI.SetActive(true);

    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("exit collision with ATM");
        ATM_UI.SetActive(false);

    }
}
