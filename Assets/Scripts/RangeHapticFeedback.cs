using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;


public class RangeHapticFeedback : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	private bool IsInCloseRange = false;
	private Renderer rend;
	public VideoClip videoClip;
	public VideoPlayer videoPlayer = null;
	public AudioSource audioSource = null;


	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance =Vector3.Distance(transform.position, player.transform.position);
		if (distance < 1.2 && IsInCloseRange == false) {
			print ("close range");
			IsInCloseRange = true;
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
			if (videoClip != null && videoPlayer == null) {
				audioSource.Stop ();
				videoPlayer = player.AddComponent<VideoPlayer> ();
				videoPlayer.playOnAwake = false;
				videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
				videoPlayer.clip = videoClip;
				videoPlayer.isLooping = true;
				videoPlayer.Play ();
			}


		}else if(distance > 1.21 && IsInCloseRange == true){
			IsInCloseRange = false;
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
			rend.sharedMaterial.SetColor ("_EmissionColor", Color.black);
			if (videoPlayer != null) {
				videoPlayer.Stop ();
				Destroy (videoPlayer);
				videoPlayer = null;
				audioSource.Play ();
			}
		}
		// videoPlayer.frame = 20;
		if (distance < 2) {
			// print ("very close range");
			//rend.sharedMaterial.SetColor ("_EmissionColor", Color.cyan);

			// rend.sharedMaterial.SetColor ("_EmissionColor", new Color ((2.0f/distance)*0.4f,(2.0f/distance)*0.4f,(2.0f/distance)*0.4f));
			// rend.sharedMaterial.SetColor ("_EmissionColor", new Color ((1.5f-0.5f)*255.0f,(1.5f-0.5f)*255.0f,(1.5f-0.5f)*255.0f));

		} else if(distance > 1.1f) {
			// rend.sharedMaterial.SetColor ("_EmissionColor", Color.black);
		} 
	}
}
