using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerHP = 100;

    public Image healthImage;

    private float time;
    private float timerCounter = 12;
    private float healthFactor;
    

    private void Start()
    {
        playerHP = 100;
        
    }

    void Update()
    {
        UpdateHealthImage();
        UpdateHp();
        HealingFactor();
    }

    private void HealingFactor()
    {
        if (playerHP < 100)
        {
            time += Time.deltaTime;
            if (time > timerCounter)
            {
                healthFactor += 10 * Time.deltaTime;
                playerHP = Mathf.RoundToInt(healthFactor);
                if (playerHP > 100)
                {
                    playerHP = 100;
                    time = 0f;
                }
            }
            else
            {
                healthFactor = playerHP;
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            playerHP -= 30;
            time = 0f;
            Debug.Log(playerHP);
            SoundManager.instance.Play(SoundManager.SoundName.playerHurt);
        }
    }

    void UpdateHp()
    {
        if (playerHP <= 0)
        {
            GameManager.instance.GameOver();
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