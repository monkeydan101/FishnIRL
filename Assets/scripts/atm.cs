using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class atm : MonoBehaviour
{
    public GameObject ATM_UI;

    public fishControler player;
    public GameObject sellMenu;
    [SerializeField] public TextMeshProUGUI moneyCountText;

    public bool sellStatus;

    // Start is called before the first frame update
    void Start()
    {
        ATM_UI.SetActive(false);

        sellMenu.SetActive(false);
        sellStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        moneyCountText.text = player.moneyCount.ToString();
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

    public void toggleSellMenu()
    {

        if (sellStatus){
            sellMenu.SetActive(false);
            sellStatus = false;
        }
        else{
            sellMenu.SetActive(true);
            sellStatus = true;
        }
    }
}
