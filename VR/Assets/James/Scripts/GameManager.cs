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

    private bool isGameOver;

    private void Start()
    {
        //test game over
        //GameOver();
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    public void GameOver()
    {
        isGameOver = true;
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