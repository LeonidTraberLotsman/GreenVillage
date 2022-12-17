using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class inter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public void Interact(CameraController controller);
    abstract public void Interact();


}

