using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationFadeInOut : MonoBehaviour {

	public Image myPanel;
	float fadeTime = 3f;
	Color colorToFadeTo;

	// Use this for initialization
	void Start () {
		colorToFadeTo = new Color (1f, 1f, 1f, 0f);
		myPanel.CrossFadeColor (colorToFadeTo, fadeTime, true, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
