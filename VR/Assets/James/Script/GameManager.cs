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
        StartCoroutine(DayTextChange());
    }

    IEnumerator DayTextChange()
    {
        dayText.SetText($"<fade>DAY {dayCount}</fade>");
        yield return new WaitForSeconds(5);
        dayText.SetText("");
    }
}
