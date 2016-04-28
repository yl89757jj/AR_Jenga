using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishConstruction : MonoBehaviour {
	public GameObject freeModeController;
	public GameObject jengaGame;
	public GameObject placementText;
	public GameObject range;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Confirm() {
		freeModeController.gameObject.GetComponent<FreeModeController> ().destructionMode = true;
		this.gameObject.SetActive (false);
	}
}
