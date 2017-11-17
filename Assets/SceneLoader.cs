using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	private bool LoadMenu = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 4) {
			Debug.Log ("Load Menu");
			if (!LoadMenu) {
				LoadMenu = true;
				iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
			}
		}

		if (LoadMenu && Input.touchCount == 0) {
			LoadMenu = false;
			SceneManager.LoadScene ("Scenes/00 Menu");
		}
	}

	public void Select (string sceneName)
	{
		Debug.Log ("Load " + sceneName);
		iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)2);
		SceneManager.LoadScene (sceneName);
	}

	public void SelectMenu ()
	{
		if (Input.GetTouch(0).pressure > 2.66f)
		{
			Debug.Log ("Load Menu");
			iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)0);
			SceneManager.LoadScene ("Scenes/00 Menu");
		}
	}


}
