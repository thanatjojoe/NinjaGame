using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayNightCycle : MonoBehaviour
{
    public List<Difficult> difficults;
    private int dayCount = 1;
    [SerializeField] private Light sun;

    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private float sunRotationSpeed;

    [Header("DateTimeSpeed")] 
    [SerializeField] private float daySpeed;
    [SerializeField] private float afterNoonSpeed;
    [SerializeField] private float nightSpeed;
    
    [Header("LightingPreset")] 
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;
    public GameObject rain;
    public bool isRain;


    private void Start()
    {
        GameManager.instance.ChangeDay();
    }

    private void Update()
    {
        dayCount = GameManager.instance.DayCount;
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if (timeOfDay > 24)
        {
            timeOfDay = 0f;
            
        }
        UpdateRotateSpeed();
        UpdateLighting();
        UpdateSunRotation();
        
    }

    private void OnValidate()
    {
        UpdateRotateSpeed();
        UpdateSunRotation();
        UpdateLighting();
    }

    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation,sun.transform.rotation.y,sun.transform.rotation.z);
        
    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }

    private void UpdateRotateSpeed()
    {
        ChangeDifficult();
        if (timeOfDay < 5)
        {
            sunRotationSpeed = nightSpeed;
        }
        else if (timeOfDay < 5.1f)
        {
            if (isRain)
            {
                rain.SetActive(false);
                isRain = false;
            }
            GameManager.instance.DayCount += 1;
            GameManager.instance.ChangeDay();
            DifficultManager.instance.onNight = false;
            DifficultManager.instance.difficultLevel = dayCount;
            DifficultManager.instance.ChangeDifficult();
            RandomRainSpawn();
            timeOfDay = 5.11f;
        }
        else if (timeOfDay < 15)
        {
            sunRotationSpeed = daySpeed;
        }
        else if (timeOfDay < 19)
        {
            sunRotationSpeed = afterNoonSpeed;
        }
        else if (timeOfDay < 24)
        {
            sunRotationSpeed = nightSpeed;
            DifficultManager.instance.onNight = true;
        }
    }

    private void RandomRainSpawn()
    {
        var rand = Random.Range(0.1f, 1f);
        if (rand >= 0.7)
        {
            rain.SetActive(true);
            isRain = true;
        }
    }
    

    private void ChangeDifficult()
    {
        int diffCount;
        if (dayCount > 7)
        {
            diffCount = 7;
        }
        else
        {
           diffCount  = dayCount - 1;
        }
        daySpeed = difficults[diffCount].daySpeed;
        afterNoonSpeed = difficults[diffCount].afterNoonSpeed;
        nightSpeed = difficults[diffCount].nightSpeed;
    }
    
}