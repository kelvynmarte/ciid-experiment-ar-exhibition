using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdGameEventSystem : MonoBehaviour {

	private static ArrayList gameFieldPolygon = new ArrayList();

	private float timeLastFieldPointAdded = 0.0f;
	
	GameObject player;
	
	
	GameObject databaseManagerGameObject;
	BirdsDatabaseManager databaseManager;
	public Text debugText1;
	public Text debugText2;

	/*
	 * Accelerometer
	 */
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 2.0f;
	float lowPassFilterFactor;
	Vector3 lowPassValue;



	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("MainCamera");
		
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
		
		databaseManagerGameObject = GameObject.FindGameObjectWithTag("Firebase");
		databaseManager = databaseManagerGameObject.GetComponent<BirdsDatabaseManager>();

	}

	// Update is called once per frame
	void Update () {
		if ((Input.touchCount == 4 || Input.GetKeyDown("p") )&& Time.fixedTime > timeLastFieldPointAdded + 2.0f) {
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)5);
			gameFieldPolygon.Add (player.transform.position);
			timeLastFieldPointAdded = Time.fixedTime;
		}
		
		// Accelerometer Create new Bird
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold || Input.GetKeyDown("n"))
		{
			databaseManager.CreateNewBird();
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
		}
	}

	public Vector3 GetPositionForNewBird(){
		if (gameFieldPolygon.Count == 0)
			return new Vector3(1.0f, 0, 0);
		int randomVal = Random.Range(0, gameFieldPolygon.Count);
		Vector3 pos = (Vector3) gameFieldPolygon [randomVal];
		debugText1.text = randomVal + " (R): " + pos.ToString();
		return pos;
	}
}
