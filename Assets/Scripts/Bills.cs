using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bills : MonoBehaviour
{
    public GameObject moneyPanel;

    public GameObject billPanel;

    public Text rent;

    public Text electricity;

    public Text TotalBill;
    public Button payBtn;

    public Button backBtn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rent.text ="Rent: $" + GameManager.Rent.ToString("F2");
        electricity.text = "Electricity Bill: $" +GameManager.totalElectricityUsage.ToString("F2");
        TotalBill.text = "$" +(GameManager.Rent + GameManager.totalElectricityUsage).ToString("F2");
        if (GameManager.isMonthEnd)
        {
            payBtn.interactable = true;
            backBtn.interactable = false;
        }
        else
        {
            payBtn.interactable = false;
            backBtn.interactable = true;

        }
    }

    public void PayBill()
    {
        GameManager.money -= (GameManager.Rent + GameManager.totalElectricityUsage);
        GameManager.totalElectricityUsage = 0;
        billPanel.SetActive(false);
        moneyPanel.SetActive(false);
        GameManager.isMonthEnd = false;
        
    }
}
