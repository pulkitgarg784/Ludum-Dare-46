using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cooler : MonoBehaviour
{
    public Server connectedServer;
    public string coolerName;
    public Transform raycastpt;

    public float coolingFactor = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (connectedServer == null)
        {
            Vector3 forward = raycastpt.forward * 1;//range
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
        connectedServer.coolingFactor -= coolingFactor;
    }

    // Update is called once per frame


  
}
