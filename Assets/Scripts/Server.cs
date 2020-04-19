using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    private GameManager _gameManager;

    public float cost;
    public float sellPrice = 75f;
    public float repairCost;
    public float electricityUsage = 1f;

    public Material RepairMat;
    public Material overheatMat;
    private Material currentMat;
    
    [HideInInspector]
    public float finalSellPrice;
    
    public string Servername;
    public int MaxSupportedVisitor = 500;
    public int currentVisitors = 0;
    public float ServerTemp = 25;
    public float Serverload;
    public bool isHeating;
    public float Heatingfactor =0.01f;
    
    public float health = 100;
    private bool isActive = true;
    [HideInInspector]
    public float coolingFactor;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        currentMat = GetComponent<MeshRenderer>().material;
        if (sellPrice == 0)
        {
            sellPrice = 0.75f * cost;
        }

        if (repairCost == 0)
        {
            repairCost = 0.5f * cost;
        }
        GameManager.VisitorLimit += MaxSupportedVisitor;
        StartCoroutine(increaseVisitorCount());
        StartCoroutine(UseElectricity());
        if (GameManager.money>=cost)
        {
            GameManager.money -= cost;
            Debug.Log("Spawned Object");
        }
        else
        {
            Destroy(gameObject);
            _gameManager.showPrompt("You do not have enough money",1.5f);
        }

    }

    private void Update()
    {
        

        finalSellPrice = health / 100 * sellPrice;
        if (health<=0)
        {
            health = 0;
            Debug.Log("Server Destroyed");
            return;
        }

        if (!isActive)
        {
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
            GetComponent<MeshRenderer>().material = overheatMat;
        }
        else if (ServerTemp<40)
        {
            GetComponent<MeshRenderer>().material = currentMat;
        }
        
        
        
    }
    public void Sell()
    {
        GameManager.money += finalSellPrice;
        _gameManager.showPrompt("You sold "+Servername+" for $"+ finalSellPrice.ToString("F2"),2);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.TotalVisitors -= currentVisitors;
        GameManager.VisitorLimit -= MaxSupportedVisitor;

        
    }

    public void RepairCaller()
    {
        if (GameManager.money>=repairCost)
        {
            GameManager.money -= repairCost;
            StartCoroutine(repair());
        }
        else
        {
            _gameManager.showPrompt("You do not have enough money for the repair",1.5f);
            
            //prompt player
        }

    }
    public IEnumerator repair()
    {
        Debug.Log("Start repair");
        isActive = false;
        GetComponent<MeshRenderer>().material = RepairMat;
        GameManager.TotalVisitors -= currentVisitors;
        GameManager.VisitorLimit -= MaxSupportedVisitor;
        
        currentVisitors = 0;

        yield return new WaitForSeconds(2);
        
        isActive = true;
        GameManager.VisitorLimit += MaxSupportedVisitor;
        StartCoroutine(increaseVisitorCount());
        ServerTemp = 25;
        health = 100;
        Serverload = 0;
        GetComponent<MeshRenderer>().material = currentMat;
        _gameManager.showPrompt("Done Repairing "+Servername,1.5f);
        
    }

    IEnumerator UseElectricity()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameManager.totalElectricityUsage += electricityUsage;
            
        }
    }
    public IEnumerator increaseVisitorCount()
    {
        if (isActive)
        {
            Debug.Log("adding visitor");


            yield return new WaitForSeconds(.05f); //delay in adding visitors


            if (currentVisitors < MaxSupportedVisitor)
            {
                currentVisitors++;
                GameManager.TotalVisitors++;
                Serverload = (float) currentVisitors * 100 / (float) MaxSupportedVisitor;

            }

            StartCoroutine(increaseVisitorCount());
        }

    }


}
