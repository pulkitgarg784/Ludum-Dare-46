using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Server : MonoBehaviour
{
    private GameManager _gameManager;
    public float cost;
    public float sellPrice = 75f;
    public float repairCost;
    public float electricityUsage = 1f;

    public Material RepairMat;

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
    
    public GameObject FireParticles;
    bool isburning;
    private bool textshown;
    private GameObject fire;
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
            if (!textshown)
            {
                _gameManager.showPrompt("Server destroyed due to overheating",2f);
                textshown = true;
            }

            Destroy(gameObject);
            return;
        }

        if (!isActive)
        {
            return;
        }
        if (ServerTemp>25)
        {
            ServerTemp -= coolingFactor*2*Time.timeScale;
        }
        if (isHeating)
        {
            if (ServerTemp<45)
            {
                ServerTemp += Heatingfactor*2*Time.timeScale;
            }
        }
        if (Serverload>75)
        {
            isHeating = true;
        }
        else
        {
            isHeating = false;
        }

        if (ServerTemp>40)
        {
            health -= (ServerTemp - 40) / 100;
            if (!isburning)
            {
                fire = Instantiate(FireParticles, transform.position, Quaternion.identity,transform);
                isburning = true;
            }
        }
        else if (ServerTemp<40)
        {
            isburning = false;
            if (fire!=null)
            {
                Destroy(fire);
            }
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

        yield return new WaitForSeconds(10); //TODO: change repair time
        
        isActive = true;
        GameManager.VisitorLimit += MaxSupportedVisitor;
        StartCoroutine(increaseVisitorCount());
        ServerTemp = 25;
        health = 100;
        Serverload = 0;
       GetComponent<MeshRenderer>().material = currentMat;
        _gameManager.showPrompt("Done Repairing "+Servername,3f);
        
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
            float min = 3f;
            float max = 10f;
            float waitTime;
            
            
            waitTime =Random.Range(min, max);
            waitTime *= GameManager.adsMultiplier;
            
            yield return new WaitForSeconds(waitTime); //delay in adding visitors //randomize


            if (currentVisitors < MaxSupportedVisitor && GameManager.adsMultiplier<0.09f)//90 perc of slider max
            {
                currentVisitors++;
                GameManager.TotalVisitors++;

            }
            else
            {
                currentVisitors--;
                GameManager.TotalVisitors--;
            }
            Serverload = (float) currentVisitors * 100 / (float) MaxSupportedVisitor;

            StartCoroutine(increaseVisitorCount());
        }

    }


}
