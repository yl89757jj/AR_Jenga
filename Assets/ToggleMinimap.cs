using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleMinimap : MonoBehaviour {
	public GameObject minimapCam;
	public GameObject toggleText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleFrontal() {
		bool toggle = minimapCam.GetComponent<MinimapController> ().frontal_mode;
		minimapCam.GetComponent<MinimapController> ().frontal_mode = !toggle;
		if (!toggle) {
			toggleText.gameObject.GetComponent<Text> ().text = "Overhead";
		} else {
			toggleText.gameObject.GetComponent<Text> ().text = "Frontal";
		}
	}
}
