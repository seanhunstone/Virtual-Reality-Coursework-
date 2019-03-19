using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSlider : MonoBehaviour {
	public GameObject[] doors;
	public float red, green, blue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RedChange(float value){
		this.red = value;
		setColour ();
	}

	public void GreenChange(float value){
		this.green = value;
		setColour ();
	}

	public void BlueChange(float value){
		this.blue = value;
		setColour ();
	}

	public void setColour(){
		GetComponent<Renderer> ().material.color = new Color (red, green, blue);

		}
}
