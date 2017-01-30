﻿using UnityEngine;
using System.Collections;

public class ThunderLightControl : MonoBehaviour {

	Light thunder;
	AudioSource audioSource;
	float randomFrameForLighting; 
	float thunderRange;
	float lightingMinusEasing;
	float lightTimeCounting = 0.0f;
	public float lightRateControl; 

	bool isLighting = false;

	public bool active;
	// Use this for initialization
	void Start () {
		active = true;
		thunder = gameObject.GetComponent<Light>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(active) {
			if (isLighting == false) {
				// if it is not lighting, randomly make one
			// pick a number between 0 and 10
				randomFrameForLighting = Random.Range (0.0f, 10.0f);
	
			// turn light on if it is smaller than lightRateControl 	
				if (randomFrameForLighting <= lightRateControl) {
					LightOn (); // turn on light
					isLighting = true;
				}
			} else {
				Lighting (); // make light weaker and weaker after turned on
			}	
		}
	}

	// this is the function suddently turn on light
	void LightOn(){
			audioSource.Play();
		// turn on light for a frame
		thunderRange = 8.0f;
		thunder.intensity = thunderRange;

		// add sound effects here if needed


	}

	// this is the function that make light weaker and weaker
	void Lighting (){
		// when there is light, lower the range each frame
		if (thunderRange > 0.0f) {
			// for a little easing
			lightTimeCounting += Time.deltaTime;
			lightingMinusEasing = Mathf.Sin(lightTimeCounting);

			// range down
			thunderRange -= lightingMinusEasing;
			thunder.intensity = thunderRange;
		
		// when range is less than 0, tell others lighting is over and we can roll another float
		} else {
			isLighting = false;
			lightTimeCounting = 0.0f;
			thunder.intensity = 0.0f;
		}
	}
}
