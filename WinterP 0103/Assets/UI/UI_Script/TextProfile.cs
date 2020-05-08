using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextProfile : MonoBehaviour {
    Text name;
    GameObject manager;

	// Use this for initialization
	void Awake () {
        name = GetComponent<Text>();
        manager = GameObject.Find("NameManager");
        name.text = manager.GetComponent<RelNameManager>().getName();
	}

}
