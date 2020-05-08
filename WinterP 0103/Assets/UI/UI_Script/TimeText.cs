using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {
    int Day, Month, Year; 
    Text timeLabel;

    // Use this for initialization
    void Awake()
    {
        timeLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Day = TimeManager.getDay();
        Month = TimeManager.getMonth();
        Year = TimeManager.getYear();
        timeLabel.text = Year.ToString() + " / " + Month.ToString() + " / " + Day.ToString();
    }
}
