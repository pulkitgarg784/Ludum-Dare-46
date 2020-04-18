using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static int VisitorLimit = 0;
    public static int TotalVisitors = 0;

    public static float money = 100;

    public Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        money+=Mathf.CeilToInt(Time.deltaTime*TotalVisitors*0.005f);
        MoneyText.text = "$ "+money;
    }


}
