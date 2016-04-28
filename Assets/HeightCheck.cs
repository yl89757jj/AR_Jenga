using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeightCheck : MonoBehaviour {
	public float targetHeight;
	public float currentHeight = 0f;
	public GameObject heightText;
//	public float maxHeight = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<FreeModeController> ().destructionMode) {
			heightText.GetComponent<Text> ().enabled = false;
			return;
		}
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Bricks");
		if (bricks != null && bricks.Length > 0) {
			float avg = 0f;
			int num = 0;
			foreach (GameObject brick in bricks) {
				if (brick.transform.parent.gameObject.name == "Jenga") {
					avg += brick.transform.position.y;
					num++;
				}
			}
			if (num > 0) {
				avg /= num;
			}
			currentHeight = avg;
//			if (currentHeight > maxHeight) {
//				maxHeight = currentHeight;
//			}
		}
		heightText.GetComponent<Text> ().text = "Height: " + currentHeight.ToString () + "/" + targetHeight.ToString ();
	}
}
