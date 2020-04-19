using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class revenuePanel : MonoBehaviour
{
    public Text CurrentVisitor;
    public Text MaxVisitor;
    public Text Money;
    public Text AdsText;
    public GameObject Panel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("up stat");
        CurrentVisitor.text = "Current Visitors: "+GameManager.TotalVisitors.ToString();
        MaxVisitor.text = "Visitors Limit: "+GameManager.VisitorLimit.ToString();
        Money.text = "Money: "+GameManager.money.ToString("F2");

    }

    public void Back()
    {
        Panel.SetActive(false);
    }

    public void AdsValue(float value)
    {
        AdsText.text = value.ToString();
        GameManager.adsMultiplier = value;
        
    }
}
