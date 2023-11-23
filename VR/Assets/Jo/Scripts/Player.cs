using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static event System.Action OnCollisionWithEnemy;
    public static int playerHP = 100;

    public Image healthImage; 

    void Update()
    {
        UpdateHealthImage();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            OnCollisionWithEnemy?.Invoke();
            Debug.Log(playerHP);
        }
    }

    void UpdateHealthImage()
    {
        if (healthImage != null)
        {
           
            float healthPercentage = (float)playerHP / 100f;

            // ปรับสีของ Image ให้มืดลงเมื่อเลือดน้อยลง
            float darkeningFactor = 1 - healthPercentage; // ปัจจุบัน, darkeningFactor เป็นค่าระหว่าง 0 ถึง 1
            darkeningFactor = Mathf.Clamp(darkeningFactor, 0f, 0.80f); // ไม่ให้มืดลงมากกว่า 75%
            Color darkenedColor = new Color(90f, 0f, 0f, darkeningFactor);

            // ตั้งค่าสีของ Image
            healthImage.color = darkenedColor;
            
        }
    }
}