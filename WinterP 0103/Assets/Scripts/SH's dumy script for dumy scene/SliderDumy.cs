using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDumy : MonoBehaviour {

    public Slider mySlider;
    public Text myText;

	// Use this for initialization
	void Start () {
        mySlider = GameObject.Find("Slider").GetComponent<Slider>();
	}
	
    

   public void ValueChange()
    {
        Debug.Log(mySlider.value);
    }
}
