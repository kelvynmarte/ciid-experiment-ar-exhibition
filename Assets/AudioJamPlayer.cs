using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioJamPlayer : MonoBehaviour {
	public Text debugText;
	Vector3 lastPosition;
	AudioSource[] audios;
	// Use this for initialization
	void Start () {
		audios = GetComponents<AudioSource>();
	}


	// Update is called once per frame
	void Update () {
		debugText.text = "X:" + this.transform.position.x + " " + "Z:" + this.transform.position.z;
		if (lastPosition.x != this.transform.position.x || lastPosition.z != this.transform.position.z) {
			if (lastPosition.x < 0.0F && this.transform.position.x > 0.0F) { 
				if (this.transform.position.z > 0.0F) { // X+ & Z+
					audios [0].Play ();
				} else { // X+ & Z-
					audios [1].Play ();
				}
			} else if (lastPosition.x > 0.0F && this.transform.position.x < 0.0F) {
				if (this.transform.position.z > 0.0F) { // X- & Z+
					audios [2].Play ();
				} else { // X- & Z-
					audios [3].Play ();
				}
			} else if (lastPosition.z < 0.0F && this.transform.position.z > 0.0F) {
				if (this.transform.position.x > 0.0F) { // X+ & Z+
					audios [0].Play ();
				} else { // X- & Z+
					audios [2].Play ();
				}
			} else if (lastPosition.z > 0.0F && this.transform.position.z < 0.0F) {
				if (this.transform.position.x > 0.0F) { // X+ & Z-
					audios [1].Play ();
				} else { // X- & Z-
					audios [3].Play ();
				}
			}
		}
		lastPosition = this.transform.position;
	}
}
