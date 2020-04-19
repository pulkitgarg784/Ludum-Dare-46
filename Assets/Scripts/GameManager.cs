using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static int VisitorLimit = 0;
    public static int TotalVisitors = 0;
    public static float totalElectricityUsage = 0;
    public static float Rent = 500;
    public static bool isMonthEnd;
    public static float money = 100;
    public static float adsMultiplier = 0.00005f;
    public Text MoneyText;
    private bool isGameover;
    public GameObject Prompt;

    public Text PromptText;
    public GameObject UICanvas;
    public GameObject HackPanel;
    public GameObject MoneyPanel;

    public GameObject BillPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomHack());

    }

    // Update is called once per frame
    void Update()
    {
        money+=Time.deltaTime*TotalVisitors*adsMultiplier;//speed of money
        MoneyText.text = "$ "+money.ToString("F2");

        if (money<0)
        {
            money = 0;
            GameOver();
            
        }
        if (isMonthEnd &&!isGameover)
        {
            MoneyPanel.SetActive(true);
            BillPanel.SetActive(true);
            Time.timeScale = 0;

        }
        else if(!isMonthEnd &&!isGameover)
        {
            Time.timeScale = 1;
        }
    }

    public void showPrompt(string prompt,float duration)
    {
        StartCoroutine(PromptUser(prompt,duration));
    }

    public IEnumerator PromptUser(string prompt, float duration)
    {
        Prompt.SetActive(true);
        PromptText.text = prompt;
        yield return new WaitForSeconds(duration);
        PromptText.text = " ";
        Prompt.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameover = true;
        Time.timeScale = 0;
    }

    IEnumerator RandomHack()
    {
        yield return new WaitForSeconds(Random.Range(10,15));

        if (GameManager.TotalVisitors>2500)
        {
            if (Random.Range(0,5)==1)
            {
                Debug.Log("<color=green>Hack</color>",this);
                GameObject hackpanel =  Instantiate(HackPanel, UICanvas.transform.position, Quaternion.identity,UICanvas.transform);
                hackpanel.transform.SetAsLastSibling();

            }
        }
        StartCoroutine(RandomHack());
    }


}
