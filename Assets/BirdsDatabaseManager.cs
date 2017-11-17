using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class BirdsDatabaseManager : MonoBehaviour {
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
			});
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
	}

	void HandleValueChanged(object sender, ValueChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// Do something with the data in args.Snapshot
		Debug.Log(args.Snapshot.GetRawJsonValue());
	}
}
