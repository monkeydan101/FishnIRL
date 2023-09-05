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
    public GameObject buyMenu;
    [SerializeField] public TextMeshProUGUI moneyCountText;
    [SerializeField] public TextMeshProUGUI moneyCountText2;

    public bool sellStatus;
    public bool buyStatus;

    // Start is called before the first frame update
    void Start()
    {
        ATM_UI.SetActive(false);

        sellMenu.SetActive(false);
        sellStatus = false;
        buyStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        moneyCountText.text = player.moneyCount.ToString();
        moneyCountText2.text = player.moneyCount.ToString();
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

    public void toggleBuyMenu()
    {

        if (buyStatus)
        {
            buyMenu.SetActive(false);
            buyStatus = false;
        }
        else
        {
            buyMenu.SetActive(true);
            buyStatus = true;
        }
    }
}
