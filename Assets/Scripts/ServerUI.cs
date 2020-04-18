using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServerUI : MonoBehaviour
{
    private Server _server;
    public GameObject canvas;
    private bool _isActive;
    public Text serverName;
    public Text heat;
    public Text load;
    public Text temp;
    public Text health;
    public Text fact;

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
                if (hit.collider.gameObject.GetComponent<Server>() != null)
                {
                    if (_server!=null)
                    {
                        if (hit.collider.gameObject.GetComponent<Server>() == _server)
                        {
                            //clicked on same server
                            Debug.Log("same");
                            if (_isActive)
                            {
                                _isActive = false;
                                canvas.SetActive(false);
                                return;
                        
                            }
                        }
                    }

                    _server = hit.collider.gameObject.GetComponent<Server>();
                    Vector3 _serverPos = _server.gameObject.transform.position;
                    transform.position = new Vector3(_serverPos.x,_serverPos.y+2,_serverPos.z);
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

      //  var n = Camera.main.transform.position - transform.position;
       // var newRotation = Quaternion.LookRotation(n) * Quaternion.Euler(90, 0, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1.0f);
        if (_server!=null)
        {
            //set stats:
            serverName.text = _server.Servername;
            heat.text = "Heating Capacity: "+(_server.Heatingfactor*1000).ToString("F2");
            load.text = "Server Load: "+_server.Serverload.ToString("F2") + "%";
            temp.text = "Temperature: "+_server.ServerTemp.ToString("F2") + "C";
            health.text ="Health: "+ _server.health.ToString("F2");
        }
    }
}
