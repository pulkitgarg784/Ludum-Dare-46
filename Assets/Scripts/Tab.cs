using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{

    public GameObject infoPanel;
    public Text InfoText;
    public void TogglePanel(GameObject panel)
    {
        if (!GameManager.isMonthEnd)
        {
            panel.SetActive(!panel.activeSelf);

        }
    }

   public void SetInfoText(string info)
    {
        infoPanel.SetActive(true);
        InfoText.text = info;
        InfoText.text = InfoText.text.Replace ("\\n", "\n");
    }
   public void HideInfoText()
    {
        infoPanel.SetActive(false);
        InfoText.text = " ";
    }


}
