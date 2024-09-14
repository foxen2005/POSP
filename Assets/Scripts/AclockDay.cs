using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.IO;

public class AclockDay : MonoBehaviour
{
    DateTime GetTimer;
    public string TimerClock;
    public float Aclock_;
   
    public string days, hour;

    public TMP_Text AclockonTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTimer = DateTime.Now;

        days = GetTimer.ToLongDateString();
            hour = GetTimer.ToLongTimeString();


        AclockonTime.text =  days+", " + hour +"hrs ";
    }
}
