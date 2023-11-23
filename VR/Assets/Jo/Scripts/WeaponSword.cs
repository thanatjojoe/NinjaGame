using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : MonoBehaviour
{
    public static float damageMultiplier = 10f; // ตัวคูณสำหรับการคำนวณดาเมจ

    void start()
    {
       
    }
    private void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // คำนวณดาเมจตามความเร็วของการเหวี่ยงดาบ
            float swordSpeed = CalculateSwordSpeed(); 
            float damage = swordSpeed * damageMultiplier;

            // แสดง Debug Log
            Debug.Log("Damage: " + damage);
            if (damage > 2)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private float CalculateSwordSpeed()
    {
        
        // คำนวณความเร็วของการเหวี่ยงดาบ

        float swordSpeed = Mathf.Abs(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch).y);
        return swordSpeed;
    }
}
