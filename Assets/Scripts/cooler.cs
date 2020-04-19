using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class cooler : MonoBehaviour
{
    public float cost = 100f;
    public float sellPrice = 75f;
    public Server connectedServer;
    public string coolerName;
    public Transform raycastpt;
    public float electricityUsage = .5f;
    public float coolingFactor = 0.01f;
    private GameManager _gameManager;

    public GameObject FireParticles;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(UseElectricity());

        if (GameManager.money>=cost)
        {
            GameManager.money -= cost;
            Debug.Log("Spawned Object");
        }
        else
        {
            _gameManager.showPrompt("You do not have enough money",1.5f);
            Destroy(gameObject);
            //prompt player
        }
        StartCoroutine(RandomFire());

    }

    private void Update()
    {
        if (connectedServer == null)
        {
            Vector3 forward = raycastpt.forward * 2;//range
            Debug.Log("Finding server");
            Debug.DrawRay(raycastpt.position, forward, Color.green);
            RaycastHit hit;
            Ray ray =new Ray(raycastpt.position,forward);
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.GetComponent<Server>()!= null)
                {
                    connectedServer = hit.collider.gameObject.GetComponent<Server>();
                    connectedServer.coolingFactor += coolingFactor;
                }
            }
        }


        
    }

    private void OnDestroy()
    {

        if (connectedServer!=null)
        {
            connectedServer.coolingFactor -= coolingFactor;
        }
    }

    public void Sell()
    {
        GameManager.money += sellPrice;
        Destroy(gameObject);
        _gameManager.showPrompt("You sold "+coolerName+" for $"+ sellPrice.ToString("F2"),2);
    }
    IEnumerator UseElectricity()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameManager.totalElectricityUsage += electricityUsage;
            
        }
    }

    IEnumerator RandomFire()
    {
        if (GameManager.TotalVisitors>2500)
        {
            if (Random.Range(0,20)==5)
            {
                Debug.Log("<color=red>Fire</color>",this);
                Instantiate(FireParticles, transform.position, Quaternion.identity,transform);
                Destroy(gameObject,10f);

            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(RandomFire());
    }

  
}
