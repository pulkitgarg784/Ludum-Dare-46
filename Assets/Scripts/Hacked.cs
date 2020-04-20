using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hacked : MonoBehaviour
{
    public float waitTime = 2;
    
    public GameObject hackedPanel;
    public GameObject fixPanel;
    public GameObject panel;
    private float startMoney;
    void Start()
    {
        StartCoroutine(SpawnPopup());
       // StartCoroutine(stealMoney());
       startMoney = GameManager.money;
    }

    private void Update()
    {
        GameManager.money -= Time.deltaTime * startMoney * .0275f; //TODO: money reduction speed
    }

    IEnumerator SpawnPopup()
    {
        for (int i = 0; i < Random.Range(15,20); i++)
        {
            

            float xPos = Random.Range(300, Screen.width-300);
            float yPos = Random.Range(300, Screen.height-300);
            Vector3 spawnPosition = new Vector3(xPos, yPos, Random.Range(10,15));
            GameObject spwanObj = Instantiate(hackedPanel, spawnPosition, Quaternion.identity) as GameObject;
            spwanObj.transform.parent = panel.transform;
            spwanObj.transform.position = spawnPosition;
            yield return new WaitForSeconds(waitTime);
        }

        GameObject fix = Instantiate(fixPanel,new Vector3(Screen.width/2,Screen.height/2,0),Quaternion.identity,panel.transform);
        fix.transform.SetSiblingIndex(1);
    }


}

