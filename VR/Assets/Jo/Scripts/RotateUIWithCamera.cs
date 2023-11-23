using UnityEngine;

public class RotateUIWithCamera : MonoBehaviour
{
    private Transform mainCameraTransform; 
    private RectTransform canvasRectTransform; 
    public float distanceFromCamera = 1f; 

    void Start()
    {
       
        mainCameraTransform = Camera.main.transform;

        
        canvasRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // ตรวจสอบว่ามี Main Camera และ Canvas ให้ทำการหมุนและติดตามตำแหน่ง
        if (mainCameraTransform != null && canvasRectTransform != null)
        {
            // หมุน Canvas ตาม Main Camera
            canvasRectTransform.rotation = Quaternion.LookRotation(mainCameraTransform.forward, mainCameraTransform.up);

            // ติดตามตำแหน่งของ Main Camera แต่ตั้งค่าให้ห่างจาก Main Camera ในแกน Z
            canvasRectTransform.position = mainCameraTransform.position + mainCameraTransform.forward * distanceFromCamera;
        }
    }
}