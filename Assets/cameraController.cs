using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform CameraTransform;
    public float moveSpeed;

    public float moveTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    
    public Vector3 newPos;
    public Vector3 newZoom;
    public Quaternion newRot;
    
    //mouse
    public Vector3 dragStart;
    public Vector3 dragEnd;

    public Vector3 RotateStart;
    public Vector3 RotateEnd;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        newRot = transform.rotation;
        newZoom = CameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        MouseInput();
    }

    void HandleInput()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical"))>0)
        {
            newPos += transform.forward * moveSpeed * Input.GetAxis("Vertical");
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal"))>0)
        {
            newPos += transform.right * moveSpeed * Input.GetAxis("Horizontal");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRot *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRot *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        newPos.x = Mathf.Clamp(newPos.x, -40, 40);
        newPos.z = Mathf.Clamp(newPos.z, -40, 40);

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,newRot,Time.deltaTime*moveTime);
        newZoom.y = Mathf.Clamp(newZoom.y, 10, 150);
        newZoom.z = Mathf.Clamp(newZoom.z, -150, -10);
        CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, newZoom, Time.deltaTime * moveTime);
    }

    void MouseInput()
    {
        if (Input.mouseScrollDelta.y !=0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float start;
            if (plane.Raycast(ray,out start))
            {
                dragStart = ray.GetPoint(start);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float point;
            if (plane.Raycast(ray,out point))
            {
                dragEnd = ray.GetPoint(point);
                newPos = transform.position + (dragStart - dragEnd);
            }
        }

        if (Input.GetMouseButtonDown(2))    
        {
            RotateStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            RotateEnd = Input.mousePosition;
            Vector3 difference = RotateStart - RotateEnd;
            RotateStart = RotateEnd;
            
            newRot*= Quaternion.Euler(Vector3.up*(-difference.x/5f));
        }
    }
}
