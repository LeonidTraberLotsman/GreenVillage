using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Architector architector;
    

    [Header("Чувствительность мыши")]
     
    public float sensitivityMouse = 200f;
    public Transform Player;
    public Transform glaz;
    int numbercherteszha;

    float xRotation = 0;

    private void Start()
    {
        Cursor. lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*sensitivityMouse; 
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse; 

        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        Player.Rotate(Vector3.up * mouseX);

        RaycastHit hit;
        if(Physics.Raycast(glaz.position,glaz.forward,out hit))
        {
           // Debug.Log(hit.point);
            //Debug.Log(hit.transform.name);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                inter item = hit.transform.gameObject.GetComponent<inter>();
                if (item != null)
                {
                    item.Interact(this);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log(hit.point);
                architector.PlaceBuilding(hit.point,0,true);
            }
        }


        
    }
}