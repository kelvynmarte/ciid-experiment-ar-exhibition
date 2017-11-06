using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHapticFeedback : MonoBehaviour {

	// Use this for initialization
	public GameObject Player;
	private bool IsInCloseRange = false;
	private Renderer rend;
	void Start () {
		Player = GameObject.FindGameObjectWithTag("MainCamera");
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		float distance =Vector3.Distance(transform.position,Player.transform.position);
		if (distance < 2 && IsInCloseRange == false) {
			print ("close range");
			IsInCloseRange = true;
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);

		}else if(distance > 2.5 && IsInCloseRange == true){
			IsInCloseRange = false;
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
			rend.sharedMaterial.SetColor ("_EmissionColor", Color.black);
		}
		if (distance < 2) {
			print ("very close range");
			//rend.sharedMaterial.SetColor ("_EmissionColor", Color.cyan);

			rend.sharedMaterial.SetColor ("_EmissionColor", new Color ((2.0f/distance)*0.4f,(2.0f/distance)*0.4f,(2.0f/distance)*0.4f));
			// rend.sharedMaterial.SetColor ("_EmissionColor", new Color ((1.5f-0.5f)*255.0f,(1.5f-0.5f)*255.0f,(1.5f-0.5f)*255.0f));
		} else {

		}
	}
}
