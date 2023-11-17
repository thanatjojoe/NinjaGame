using UnityEngine;

public class RotateUIWithCamera : MonoBehaviour
{
    private Transform mainCameraTransform; // เก็บ Transform ของ Main Camera
    private RectTransform canvasRectTransform; // เก็บ RectTransform ของ Canvas
    public float distanceFromCamera = 1f; // ระยะห่างจาก Main Camera

    void Start()
    {
        // หา Main Camera ใน Scene
        mainCameraTransform = Camera.main.transform;

        // หา RectTransform ของ Canvas
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