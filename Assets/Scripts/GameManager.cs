using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{

    public float StartMoney = 100f;
    public static int VisitorLimit = 0;
    public static int TotalVisitors = 0;
    public static float totalElectricityUsage = 0;
    public static float Rent = 500;
    public static bool isMonthEnd;
    public static float money = 100;
    public static float adsMultiplier = 0.01f;
    public Text MoneyText;
    private bool isGameover;
    public GameObject Prompt;

    public Text PromptText;
    public GameObject UICanvas;
    public GameObject HackPanel;
    public GameObject MoneyPanel;

    public GameObject BillPanel;


    private int desiredSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomHack());
        desiredSpeed = 1;
        money = StartMoney;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;            
    }


    // Update is called once per frame
    void Update()
    {

        money+=Time.deltaTime*TotalVisitors*adsMultiplier*0.02f*Time.timeScale;//speed of money
        MoneyText.text = "$ "+money.ToString("F2");
        if (TotalVisitors>VisitorLimit)
        {
            TotalVisitors = VisitorLimit;
        }
        if (TotalVisitors <0)
        {
            TotalVisitors = 0;
        }
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
            Time.timeScale = desiredSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            desiredSpeed = 1;
            showPrompt("Time Speed = 1",2f);
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            desiredSpeed = 5;
            showPrompt("Time Speed = 5",2f);

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
        yield return new WaitForSecondsRealtime(duration);
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

        if (GameManager.TotalVisitors>1000)
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
