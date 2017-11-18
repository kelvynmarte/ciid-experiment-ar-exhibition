using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	
	public string uuid;
	public string name;
	public bool captured;

	public Bird(string name) {
		this.uuid = System.Guid.NewGuid ().ToString ();
		this.name = name;
		this.captured = false;


	}

	public Bird(string uuid, string name, bool captured) {
		this.uuid = uuid;
		this.name = name;
		this.captured = captured;
	}

	public string ToString(){
		return this.uuid + " " + this.name + " " + this.captured;
	}
}


