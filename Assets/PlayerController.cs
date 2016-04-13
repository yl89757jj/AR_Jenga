using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject selectedBrick;

	public override void OnStartLocalPlayer()
	{
		GameObject.Find ("GameController").GetComponent<GameController> ().currentPlayer = this.gameObject;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		if (selectedBrick == null)
			return;
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		CmdMove (x, z);
	}

	[Command]
	void CmdMove(float x, float z) {
		selectedBrick.gameObject.transform.Translate(x, 0, z);
	}
}
