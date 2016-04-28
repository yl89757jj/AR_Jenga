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
		scoreBoard.GetComponent<Text> ().text = "Score: " + score.ToString ();
	}
}
