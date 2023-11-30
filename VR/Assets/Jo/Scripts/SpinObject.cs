using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpinObject : XRGrabInteractable
{
    public float rotationSpeed = 50f;
    private bool isGrab = false;
    

    void Start()
    {
      
        
    }

    void Update()
    {
        if (isGrab)
        {
            Rotate();
        }
    }

    void Rotate()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    public void GrabObject()
    {
        isGrab = true;
       
    }

    public void NotGrabObject()
    {
        isGrab = false;
 
    }
}