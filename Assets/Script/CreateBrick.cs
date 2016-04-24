using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateBrick : MonoBehaviour {

	public GameObject darkBrick;
	public GameObject lightBrick;
	public GameObject spawnRegion;
	public GameObject pickupText;
	public GameObject jengaGame;
	private GameObject recentCreated;
	private Vector3 spawnLocation;
	private bool readyToCreate = true;
	// Use this for initialization
	void Start () {
		pickupText.GetComponent<Text> ().enabled = false;
		spawnLocation = new Vector3 (-15.56f, 1.84f, -13.92f);
	}
	
	// Update is called once per frame
	void Update () {
		if (recentCreated != null && recentCreated.transform.parent.gameObject.name != "SpawnRegion") {
			pickupText.GetComponent<Text> ().enabled = false;
			readyToCreate = false;
		} else {
			readyToCreate = true;
		}
	}

	public void AddBrick() {
		if (!readyToCreate)
			return;
		float random = Random.value;
		GameObject newBrick = null;
		Vector3 offset = jengaGame.gameObject.transform.position;
		if (random > 0.5f) {
			newBrick = (GameObject) Instantiate (darkBrick, spawnLocation+offset, Quaternion.identity);
		} else {
			newBrick = (GameObject) Instantiate (lightBrick, spawnLocation+offset, Quaternion.identity);
		}
		newBrick.GetComponent<Rigidbody> ().isKinematic = true;
		newBrick.GetComponent<Rigidbody> ().useGravity = true;
		pickupText.GetComponent<Text> ().enabled = true;
		newBrick.gameObject.transform.SetParent (spawnRegion.gameObject.transform);
		recentCreated = newBrick;
	}
}
