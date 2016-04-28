using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public int score;
	public GameObject scoreBoard;
	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreBoard.GetComponent<Text> ().enabled) {
			scoreBoard.GetComponent<Text> ().text = "Score: " + score.ToString ();
		}
	}

	public void getResult() {
		int total = 0;
		Vector3 origin = GameObject.Find ("Jenga").transform.position;
		GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Bricks");
		foreach (GameObject brick in bricks) {
			Vector3 pos = brick.transform.position;
			Vector2 diff = new Vector2 (pos.x - origin.x, pos.z - origin.z);
			if (diff.magnitude < 8.85f) {	//yellow region
				brick.GetComponent<Renderer> ().material.color = Color.yellow;
				total += 50;
			} else if (diff.magnitude < 14.9f) {	//red region
				brick.GetComponent<Renderer> ().material.color = Color.red;
				total += 20;
			}
		}
		score += total;
	}
}
