using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphereControl : MonoBehaviour {

	public float currentScale = 1.0f;
	public float maxScale = 1.02f;
	public float scaleDirection = 1.0f;
	public float scaleSteps = 0.0004f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		/*
		this.transform.localScale += new Vector3 (scaleSteps,scaleSteps,scaleSteps) * scaleDirection;
		if (this.transform.localScale.x < 1.0f || this.transform.localScale.x > maxScale) {
			this.scaleDirection = this.scaleDirection * -1.0f;
		}*/
	}
}