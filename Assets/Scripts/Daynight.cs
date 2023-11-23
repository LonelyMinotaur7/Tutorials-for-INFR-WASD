using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EDayNight
{
    Day,
    Night,
    noon,
}

public class Daynight : MonoBehaviour
{

    public Material DaySky;
    public Material NightSky;
    public Material NoonSky;
   
    
   [field: SerializeField] public EDayNight DayNightType { get; private set; }

   //[SerializeField] public EDayNight EDayNights;

   private void Update()
   {
       //Debug.Log(DayNightType);

       if (Input.GetKeyDown(KeyCode.B))
       {
           DayNightType = EDayNight.noon;
       }
       if (Input.GetKeyDown(KeyCode.N))
       {
           DayNightType = EDayNight.Night;
       }
       if (Input.GetKeyDown(KeyCode.M))
       {
           DayNightType = EDayNight.Day;
       }
       
       
       
       if (DayNightType == EDayNight.Day)
       {
           RenderSettings.skybox = DaySky;
       }
       if (DayNightType == EDayNight.Night)
       {
           RenderSettings.skybox = NightSky;
       }
       if (DayNightType == EDayNight.noon)
       {
           RenderSettings.skybox = NoonSky;
       }
   }
}
