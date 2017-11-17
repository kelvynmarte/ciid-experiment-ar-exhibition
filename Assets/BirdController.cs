using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {


	// Use this for initialization
	Bird bird
	{
		get { return bird; }
		set { bird = value; }
	}
	GameObject player;
	AudioSource audioSource = null;


	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
		audioSource = this.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		float distance =Vector3.Distance(transform.position, player.transform.position);
		if (distance < 0.4F) { // captured
			audioSource.Stop ();
		}
	}


}
