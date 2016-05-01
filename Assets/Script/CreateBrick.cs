using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateBrick : MonoBehaviour {

	public GameObject darkBrick;
	public GameObject lightBrick;
	public GameObject spawnRegion;
	public GameObject pickupText;
	public GameObject newBrickText;
	public GameObject jengaGame;
	public GameObject freeModeController;

	private GameObject recentCreated;
	private Vector3 spawnLocation;
	private bool readyToCreate = true;
	// Use this for initialization
	void Start () {
		pickupText.GetComponent<Text> ().enabled = false;
		spawnLocation = new Vector3 (-11.5f, 1.84f, -10.3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (recentCreated != null && recentCreated.transform.parent != null && recentCreated.transform.parent.gameObject.name == "SpawnRegion") {
			pickupText.GetComponent<Text> ().enabled = true;
			readyToCreate = false;
		} else {
			pickupText.GetComponent<Text> ().enabled = false;
			readyToCreate = true;
		}
		if (freeModeController.GetComponent<FreeModeController> ().numberOfBricks == 0) {
			pickupText.GetComponent<Text> ().enabled = false;
			this.gameObject.SetActive (false);
		}
	}

	public void AddBrick() {
		if (!readyToCreate) {
			StartCoroutine(displayText (newBrickText));
			return;
		}
		float random = Random.value;
		GameObject newBrick = null;
		Vector3 offset = jengaGame.gameObject.transform.position;
		Quaternion rot = jengaGame.gameObject.transform.rotation;
		if (random > 0.5f) {
			newBrick = (GameObject) Instantiate (darkBrick, rot*spawnLocation+offset, Quaternion.identity*rot);
		} else {
			newBrick = (GameObject) Instantiate (lightBrick, rot*spawnLocation+offset, Quaternion.identity*rot);
		}
		newBrick.GetComponent<Rigidbody> ().isKinematic = true;
		newBrick.GetComponent<Rigidbody> ().useGravity = true;
		pickupText.GetComponent<Text> ().enabled = true;
		newBrick.gameObject.transform.SetParent (spawnRegion.gameObject.transform);
		recentCreated = newBrick;
		freeModeController.GetComponent<FreeModeController> ().numberOfBricks--;
	}

	IEnumerator displayText(GameObject textObject) {
		textObject.GetComponent<Text> ().enabled = true;
		yield return new WaitForSeconds (2.0f);
		textObject.GetComponent<Text> ().enabled = false;
	}
}
