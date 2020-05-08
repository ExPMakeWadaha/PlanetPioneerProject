using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRequirmentText : MonoBehaviour {
    Text requirment;
    int human, money;

    void Awake()
    {
        requirment = GetComponent<Text>();
    }
    void Update()
    {
       int human = 3 * (int)Mathf.Pow(10, BuildManager.stage);
       int money = 3 * (int)Mathf.Pow(10, BuildManager.stage);
       requirment.text = "신도 " + human.ToString() + ", 돈 " + money.ToString();
	}
}
