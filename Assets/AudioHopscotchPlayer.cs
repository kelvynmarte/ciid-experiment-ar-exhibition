using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioHopscotchPlayer : MonoBehaviour {

	public GameObject[] objectsToKillOnTouch;

	float timeSinceLastHop = 0.0F;

	public Text debugText1;
	public Text debugText2;

	Vector3 lastPosition;
	AudioSource[] audios;

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold1 = 1.0f;
	float shakeDetectionThreshold2 = 2.0f;

	float lowPassFilterFactor;
	Vector3 lowPassValue;

	void Start () {
		audios = GetComponents<AudioSource>();

		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;

		shakeDetectionThreshold1 *= shakeDetectionThreshold1;
		shakeDetectionThreshold2 *= shakeDetectionThreshold2;

		lowPassValue = Input.acceleration;
	}


	// Update is called once per frame
	void Update () {
		debugText1.text = "X:" + this.transform.position.x + " " + "Z:" + this.transform.position.z;

		// 

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;


		timeSinceLastHop += Time.deltaTime;

		debugText2.text = "Acceleration " + deltaAcceleration.sqrMagnitude;
		if (timeSinceLastHop > 0.4F) {
			if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold1) {
				// Perform your "shaking actions" here. If necessary, add suitable
				// guards in the if check above to avoid redundant handling during
				// the same shake (e.g. a minimum refractory period).

				timeSinceLastHop = 0.0F;
				// audios [0].Play ();

				if (this.transform.position.x > 0.0F) { // X+ & Z+
					if (this.transform.position.z > 0.0F) { // X+ & Z+
						audios [0].Play ();
					} else { // X+ & Z-
						audios [1].Play ();
					}
				} else { // X- & Z+
					if (this.transform.position.z > 0.0F) { // X- & Z+
						audios [2].Play ();
					} else { // X- & Z-
						audios [3].Play ();
					}
				}
			}
		}

		if (Input.touchCount >= 2) { // deactivate game objects
			foreach(GameObject item in objectsToKillOnTouch){
				item.SetActive (false);
			}
		}
	}
}
