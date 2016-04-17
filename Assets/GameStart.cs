using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public bool gameStarted = false;
	public GameObject startText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		startText.SetActive (!gameStarted);
	}
}
