using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BrickBehavior : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		Debug.Log ("click");
		GameObject.Find ("GameController").GetComponent<GameController> ().currentPlayer.GetComponent<PlayerController> ().selectedBrick = this.gameObject;
	}
}
