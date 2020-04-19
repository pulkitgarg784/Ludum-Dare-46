using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    public Camera cam;
    public LayerMask layer;

    public ObjectSelector selector;

    private GameObject preview;
    private ObjectPreview previewScript;

    public bool isBuilding = false;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBuilding && previewScript.CanBuild())
        {
            BuildIt();
        }

        if (Input.GetMouseButtonDown(1) && isBuilding)
        {
            StopBuild();
        }

        if (Input.GetKeyDown(KeyCode.R) && isBuilding)
        {
            preview.transform.Rotate(0f, 90f, 0f);
        }

        if (isBuilding)
        {
            DoRay();
        }
    }

    public void NewBuild(GameObject _go) 
    {
        preview = Instantiate(_go, Vector3.zero, Quaternion.identity);
        previewScript = preview.GetComponent<ObjectPreview>();
        isBuilding = true;
    }

    private void StopBuild()
    {
        Destroy(preview);
        preview = null;
        previewScript = null;
        isBuilding = false;
    }

    private void BuildIt()
    {
        previewScript.Build();
        StopBuild();
    }

    private void DoRay()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            PositionObj(hit.point);
        }
    }

    private void PositionObj(Vector3 _pos)
    {
        
        float x =  Mathf.Round(_pos.x * 2f) * 0.5f;
        float y = (preview.transform.localScale.y / 2)+0.5f;
        float z =  Mathf.Round(_pos.z * 2f) * 0.5f;
        
        preview.transform.position = new Vector3(x, y, z);


    }


    public bool GetIsBuilding()//just returns the isBuilding bool, so it cant get changed by another script
    {
        return isBuilding;
    }

}


