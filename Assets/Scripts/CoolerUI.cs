using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoolerUI : MonoBehaviour
{
    private cooler _cooler;
    public GameObject canvas;
    private bool _isActive;
    public Text coolerName;
    public Text coolText;


    void Start()
    {
        canvas.SetActive(false);
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<cooler>() != null)
                {
                    if (_cooler!=null)
                    {
                        if (hit.collider.gameObject.GetComponent<cooler>() == _cooler)
                        {
                            //clicked on same cooler
                            Debug.Log("same");
                            if (_isActive)
                            {
                                _isActive = false;
                                canvas.SetActive(false);
                                return;
                        
                            }
                        }
                    }

                    _cooler = hit.collider.gameObject.GetComponent<cooler>();
                    Vector3 _coolerPos = _cooler.gameObject.transform.position;
                    transform.position = new Vector3(_coolerPos.x,_coolerPos.y+2,_coolerPos.z);
                    _isActive = true;
                    canvas.SetActive(true);

                }
                else
                {
                    if (_isActive)
                    {
                        _isActive = false;
                        canvas.SetActive(false);
                        return;
                        
                    }
                }
            }
        }

        if (_cooler!=null)
        {
            //set stats:
            coolerName.text = _cooler.coolerName;
            coolText.text = "Cooling Capacity: "+(_cooler.coolingFactor*1000).ToString("F2");

        }
    }
}
