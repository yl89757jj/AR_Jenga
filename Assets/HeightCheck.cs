using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeightCheck : MonoBehaviour {
	public float targetHeight;
	public float currentHeight = 0f;
	public GameObject heightText;
	private Vector3 centerOffset = new Vector3 (2.287587f, 0.377825f, 0.7604125f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<FreeModeController> ().destructionMode) {
			heightText.GetComponent<Text> ().enabled = false;
		}
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Bricks");
		if (bricks != null && bricks.Length > 0) {
			float maxH = 0f;
			foreach (GameObject brick in bricks) {
				if (brick.transform.parent != null && brick.transform.parent.gameObject.name == "Jenga") {
					GameObject tower = brick.transform.parent.gameObject;
					Vector3 cord = brick.transform.position;
					Vector3 rotCord = brick.transform.rotation * centerOffset;
					float h = cord.y + rotCord.y - tower.transform.position.y;
					if (h > maxH) {
						maxH = h;
					}
				}
			}
			currentHeight = maxH;
		}
		int len = currentHeight.ToString ().Length;
		string heightStr = currentHeight.ToString ().Substring (0, Mathf.Min (4, len));
		heightText.GetComponent<Text> ().text = "Height: " + heightStr + "/" + targetHeight.ToString ();
	}
}
