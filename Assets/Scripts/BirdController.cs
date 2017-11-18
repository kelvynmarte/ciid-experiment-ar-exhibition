using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	private ArrayList birdChangedListener = new ArrayList();
	public Bird bird;
	GameObject player;
	GameObject databaseManagerGameObject;
	AudioSource audioSource = null;
	BirdsDatabaseManager databaseManager;


	void Start () {
		player = GameObject.FindGameObjectWithTag("MainCamera");
		audioSource = this.GetComponent<AudioSource> ();
		databaseManagerGameObject = GameObject.FindGameObjectWithTag("Firebase");

		databaseManager = databaseManagerGameObject.GetComponent<BirdsDatabaseManager>();
	}

	// Update is called once per frame
	void Update () {
		float distance =Vector2.Distance(new Vector2(transform.position.x, transform.position.z),new Vector2(player.transform.position.x, player.transform.position.z));
		if (distance < 0.4F) { // captured
			Debug.Log("Bird captured");
			// audioSource.Stop (); // TODO put back but should not be needed
			Debug.Log(bird.name);
			bird.captured = true;
			// databaseManager.HandleBirdChanged (this, bird);
			foreach (BirdChangedListener bcl in birdChangedListener) {
				bcl.HandleBirdChanged (this, bird);
				
			}
		}
	}
	
	public void AddBirdChangedListener(BirdChangedListener bl){
		Debug.Log(bl);
		birdChangedListener.Add (bl);
	}

	public void RemoveBirdChangedListener(BirdChangedListener bl){
		birdChangedListener.Remove (bl);
	}
	

}

public interface BirdChangedListener
{
	void HandleBirdChanged(object sender, Bird b);
}
