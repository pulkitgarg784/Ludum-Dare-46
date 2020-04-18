using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static int VisitorLimit = 0;
    public static int TotalVisitors = 0;

    public static float money = 100;
    public static float adsMultiplier = 0.00005f;
    public Text MoneyText;

    public GameObject Prompt;

    public Text PromptText;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        money+=Time.deltaTime*TotalVisitors*adsMultiplier;//speed of money
        MoneyText.text = "$ "+money.ToString("F2");
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


}
