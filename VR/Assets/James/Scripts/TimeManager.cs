using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    public enum TimeDate
    {
        Sunset,
        Day,
        Afternoon,
        Night,
        Default,
    }

    public Transform lightDirection;
    public TimeDate nextTime;
    private float timeSpeed;
    public float sunsetSpeed;
    public float daySpeed;
    public float afterNoonSpeed;
    public float nightSpeed;

    private void FixedUpdate()
    {
        if (nextTime == TimeDate.Default)
        {
            lightDirection.rotation = Quaternion.Euler(0, 0, 0);
            nextTime = TimeDate.Sunset;
        }
        else if (nextTime == TimeDate.Sunset)
        {
            timeSpeed = sunsetSpeed;
            lightDirection.rotation = Quaternion.Slerp(lightDirection.rotation,Quaternion.Euler(90,0,0),Time.fixedDeltaTime * timeSpeed);
            if (lightDirection.rotation.x >= 90)
            {
                nextTime = TimeDate.Day;
            }
        }
        else if (nextTime == TimeDate.Day)
        {
            timeSpeed = daySpeed;
            lightDirection.rotation = Quaternion.Slerp(lightDirection.rotation,Quaternion.Euler(180,0,0),Time.fixedDeltaTime  * timeSpeed);
            if (lightDirection.rotation.x >= 180)
            {
                nextTime = TimeDate.Afternoon;
            }
        }
        else if (nextTime == TimeDate.Afternoon)
        {
            timeSpeed = afterNoonSpeed;
            lightDirection.rotation = Quaternion.Slerp(lightDirection.rotation,Quaternion.Euler(270,0,0),Time.fixedDeltaTime  * timeSpeed);
            if (lightDirection.rotation.x >= 270)
            {
                nextTime = TimeDate.Night;
            }
        }
        else if (nextTime == TimeDate.Night)
        {
            timeSpeed = nightSpeed;
            lightDirection.rotation = Quaternion.Slerp(lightDirection.rotation,Quaternion.Euler(360,0,0),Time.fixedDeltaTime  * timeSpeed);
            if (lightDirection.rotation.x >= 360)
            {
                nextTime = TimeDate.Default;
            }
        }
    }
}
