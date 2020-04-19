using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public GameObject serverShop;
    public GameObject coolerShop;
    public GameObject MoneyPanel;

    public void TogglePanel(GameObject panel)
    {
        if (!GameManager.isMonthEnd)
        {
            panel.SetActive(!panel.activeSelf);

        }
    }


}
