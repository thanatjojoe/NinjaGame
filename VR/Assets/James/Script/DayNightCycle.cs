using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameManager.instance.ChangeDay();
            GameManager.instance.DayCount += 1;
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
