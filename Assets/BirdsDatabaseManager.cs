using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class BirdsDatabaseManager : MonoBehaviour {

	Dictionary<string, Bird> allBirds = new Dictionary<string, Bird>();

	Dictionary<string, GameObject> freeBirdsIntances = new Dictionary<string, GameObject>();

	public Transform birdPrefab;

	DatabaseReference reference;
	// Use this for initialization
	void Start () {
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ciid-final-project.firebaseio.com/");

		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;

		FirebaseDatabase.DefaultInstance
			.GetReference("birds")
			.ChildChanged += HandleChildChanged;

		FirebaseDatabase.DefaultInstance
			.GetReference("birds")
			.ChildAdded += HandleChildAdded;

		/*
		Bird newBird = new Bird ("Seagul");

		string json = JsonUtility.ToJson(newBird);
		reference.Child("birds").Child(newBird.uuid).SetRawJsonValueAsync(json).ContinueWith(task => {
			if (task.IsFaulted) {
				// Handle the error...
				Debug.Log("Error could not save data");

			}
			else if (task.IsCompleted) {
				// Do something with snapshot...
				Debug.Log("Saved data");
			}
		});

		FirebaseDatabase.DefaultInstance
			.RootReference
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					// Handle the error...
					Debug.Log("Error could not load data");
					Debug.Log(task.ToString());

				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					// Do something with snapshot...
					Debug.Log("Loaded data");
					Debug.Log(snapshot.GetRawJsonValue());
				}
			});*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleChildChanged(object sender, ChildChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// Do something with the data in args.Snapshot
		Debug.Log(args.Snapshot.GetRawJsonValue());

		Bird changedBird = new Bird (args.Snapshot.Child("uuid").Value.ToString(), args.Snapshot.Child("name").Value.ToString(), (bool) args.Snapshot.Child("captured").Value);

		processBirdChanged (changedBird);
	}

	void HandleChildAdded(object sender, ChildChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// Do something with the data in args.Snapshot
		Debug.Log("Child added");

		Bird newBird = new Bird (args.Snapshot.Child("uuid").Value.ToString(), args.Snapshot.Child("name").Value.ToString(), (bool) args.Snapshot.Child("captured").Value);

		processBirdChanged (newBird);


	}

	void processBirdChanged(Bird changedBird){

		Debug.Log (changedBird.ToString ());
		allBirds [changedBird.uuid] = changedBird;
		if (changedBird.captured == false && freeBirdsIntances.ContainsKey(changedBird.uuid) == false) { // new
			GameObject instance = Instantiate(birdPrefab, new Vector3(2.0F, 0, 0), Quaternion.identity).gameObject;
			freeBirdsIntances.Add (changedBird.uuid, instance);
		}else if (changedBird.captured == true && freeBirdsIntances.ContainsKey(changedBird.uuid) == true){ // remove instance
			Destroy (freeBirdsIntances[changedBird.uuid]);
			freeBirdsIntances.Remove(changedBird.uuid);
		}
	}
}
