using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadMovieFeedbackControl : MonoBehaviour {

	public Slider slider1;
	public Slider slider2;
	public Slider slider3;
	public Slider slider4;

	private GameObject player;
	private GameObject[] frames;
	private float nearDistance = 1.2f;
	private float farDistance = 1.6f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
		frames = GameObject.FindGameObjectsWithTag("Frame");
	}
	
	// Update is called once per frame
	void Update () {
		float nearestFrameDistance = 10.0f;
		foreach (GameObject frame in frames) {
			float distance = Vector3.Distance(frame.transform.position, player.transform.position);
			nearestFrameDistance = Mathf.Min (nearestFrameDistance, distance);
		}
		Debug.Log (nearestFrameDistance);
		if (nearestFrameDistance < farDistance) {
			float progress = (nearestFrameDistance - nearDistance) / (farDistance - nearDistance);
			float progressSlider1 = Mathf.Min (progress, 0.32f) / 0.32f;
			slider1.value = progressSlider1;
			float progressSlider2 = Mathf.Min (Mathf.Max(progress, 0.32f)-0.32f, 0.18f) / 0.18f;
			slider2.value = progressSlider2;
			float progressSlider3 = Mathf.Min (Mathf.Max(progress, 0.50f)-0.50f, 0.32f) / 0.32f;
			slider3.value = progressSlider3;
			float progressSlider4 = Mathf.Min (Mathf.Max(progress, 0.82f)-0.82f, 0.18f) / 0.18f;
			slider4.value = progressSlider4;
		} else {
			slider1.value = 0.0f;
			slider2.value = 0.0f;
			slider3.value = 0.0f;
			slider4.value = 0.0f;
		}

	}
}
