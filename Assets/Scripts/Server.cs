using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    public GameManager _gameManager;
    public string Servername;
    public int MaxSupportedVisitor = 500;
    public int currentVisitors = 0;
    public float ServerTemp = 25;
    public float Serverload;
    public bool isHeating;
    public float Heatingfactor =0.01f;
    
    public float health = 100;
    [HideInInspector]
    public float coolingFactor;
    private void Start()
    {
        GameManager.VisitorLimit += MaxSupportedVisitor;
        StartCoroutine(increaseVisitorCount());

    }

    private void Update()
    {
        if (health<=0)
        {
            health = 0;
            Debug.Log("Server Destroyed");
            return;
        }
        if (ServerTemp>25)
        {
            ServerTemp -= coolingFactor;
        }
        if (isHeating)
        {
            if (ServerTemp<45)
            {
                ServerTemp += Heatingfactor;
            }
        }
        if (Serverload>75)
        {
        
            isHeating = true;
        }

        if (ServerTemp>40)
        {
            health -= (ServerTemp - 40) / 200;
        }
        
        
        
    }

    public IEnumerator increaseVisitorCount()
    {           
        yield return new WaitForSeconds(.01f);


        if (currentVisitors<MaxSupportedVisitor)
        {
            currentVisitors++;
            GameManager.TotalVisitors++;
            Serverload = (float)currentVisitors *100/ (float)MaxSupportedVisitor;

        }


        StartCoroutine(increaseVisitorCount());

    }


}
