using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpinObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // ความเร็วในการหมุน
    
    private bool spin = false;
    
    void Update()
    {
        if (spin == true)
        {
            Rotate();
        }
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spin = true;
        }
    }
}