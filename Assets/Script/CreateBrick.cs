using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateBrick : MonoBehaviour {

	public GameObject darkBrick;
	public GameObject lightBrick;
	public GameObject spawnRegion;
	public GameObject pickupText;
	private GameObject recentCreated;
	// Use this for initialization
	void Start () {
		pickupText.GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (recentCreated != null && recentCreated.transform.parent.gameObject.name != "SpawnRegion") {
			pickupText.GetComponent<Text> ().enabled = false;
		}
	}

	public void AddBrick() {
		float random = Random.value;
		GameObject newBrick = null;
		Vector3 spawnLocation = spawnRegion.transform.position;
		spawnLocation.z += 0.1f;
		if (random > 0.5f) {
			newBrick = (GameObject) Instantiate (darkBrick, spawnLocation, Quaternion.identity);
		} else {
			newBrick = (GameObject) Instantiate (lightBrick, spawnLocation, Quaternion.identity);
		}
		newBrick.GetComponent<Rigidbody> ().isKinematic = true;
		pickupText.GetComponent<Text> ().enabled = true;
		newBrick.gameObject.transform.SetParent (spawnRegion.gameObject.transform);
		recentCreated = newBrick;
	}
}
