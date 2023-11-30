using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public TextAnimator_TMP highScoreText;
    public int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (highScoreText != null)
        {
            highScoreText.SetText($"You survive <rainb a=0.1>{highScore}</rainb> days");
        }
    }
}
