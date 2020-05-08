using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Value_Update : MonoBehaviour {
    int human;
    int humanLimit;
    Text humanLabel;

	// Use this for initialization
	void Awake () {
        humanLabel = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        human = BuildManager.getHuman();
        humanLimit = BuildManager.getHumanLimit() * BuildManager.getHome();
        humanLabel.text = human.ToString() + " / " + humanLimit.ToString();
	}
}
