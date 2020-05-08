using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {
    Button myButton;
    Image myImage;
    public Sprite sp1, sp2;

    // Use this for initialization
	void Start () {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (BuildManager.getHuman() >= BuildManager.getBuild() && BuildManager.getMoney() >= BuildManager.getBuild())
        {
            myImage.sprite = sp1;
            myButton.interactable = true;
        }
        else
        {
            myImage.sprite = sp2;
            myButton.interactable = false;
        }
	}
}
