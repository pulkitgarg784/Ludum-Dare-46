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
        panel.SetActive(!panel.activeSelf);
    }


}
