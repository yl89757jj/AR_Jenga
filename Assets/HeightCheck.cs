using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeightCheck : MonoBehaviour {
	public float targetHeight;
	public float currentHeight = 0f;
	public GameObject heightText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
			avg /= num;
			currentHeight = avg;
		}
		heightText.GetComponent<Text> ().text = "Height: " + currentHeight.ToString () + "/" + targetHeight.ToString ();
	}
}
