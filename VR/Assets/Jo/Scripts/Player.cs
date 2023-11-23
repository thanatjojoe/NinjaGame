using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static event System.Action OnCollisionWithEnemy;
    public static int playerHP = 100;

    public Image healthImage;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        UpdateHealthImage();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            //OnCollisionWithEnemy?.Invoke();
            playerHP -= 30;
            Debug.Log(playerHP);
        }
    }

    void UpdateHp()
    {
        if (playerHP < 0)
        {
            _gameManager.GameOver();
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