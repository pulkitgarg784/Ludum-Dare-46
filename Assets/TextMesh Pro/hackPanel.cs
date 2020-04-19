using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void close()
    {
        Destroy(gameObject);    
    }

    public void Fix()
    {
        Destroy(transform.parent.gameObject);
    }
}
