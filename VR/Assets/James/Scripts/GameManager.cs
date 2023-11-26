using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int dayCount = 1;
    [SerializeField] private TextAnimator_TMP dayText;
    [SerializeField] private TextAnimator_TMP soulText; 
    [SerializeField] private int soulPoint;
    private float soulPointCounter;
    private bool isGameOver;
    public bool onShop;

    private void Start()
    {
        //test game over
        //GameOver();
        
        DifficultManager.instance.difficultLevel = dayCount;
        DifficultManager.instance.ChangeDifficult();
    }

    private void Update()
    {
        SoulPointCalculate();
    }

    private void SoulPointCalculate()
    {
        if (onShop)
        {
            if (soulPoint > 0)
            {
                SoulShop.instance.AddSoul(soulPoint);
                soulPoint = 0;
            }
            
            if (soulPointCounter > soulPoint)
            {
                onShop = true;
                soulPointCounter -= 100 * Time.deltaTime;
            }
            else
            {
                soulPointCounter = soulPoint;
                onShop = false;
            }
        }
        else
        {
            if (soulPointCounter < soulPoint)
            {
                soulPointCounter += 100 * Time.deltaTime;
            }
            else
            {
                soulPointCounter = soulPoint;
            }
        }
        soulText.SetText(Convert.ToInt32(soulPointCounter).ToString());
    }

    public void IncreaseSoul(int soulIncrease)
    {
        soulPoint += soulIncrease;
    }

    public void DecreaseSoul(int soulDecrease)
    {
        soulPoint -= soulDecrease;
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(GameIsOver());
    }

    

    IEnumerator GameIsOver()
    {
        //show game over canvas
        yield return new WaitForSeconds(5f);
        Debug.Log("Game Over");
    }
    public int DayCount
    {
        get { return dayCount; }
        set { dayCount = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeDay()
    {
        dayText.SetText($"<wave a=0.1>DAY {dayCount}");
    }

    IEnumerator DayTextChange()
    {
        dayText.SetText($"<wave a=0.1>DAY {dayCount}");
        yield return new WaitForSeconds(5);
        dayText.SetText($"DAY {dayCount}");
    }
}