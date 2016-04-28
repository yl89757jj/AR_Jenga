using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishConstruction : MonoBehaviour {
	public GameObject freeModeController;
	public GameObject heightWarning;
	public GameObject lastPieceWarning;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Confirm() {
		if (GameObject.Find ("Select").transform.childCount == 0) {
			if (freeModeController.gameObject.GetComponent<HeightCheck> ().currentHeight
			    > freeModeController.gameObject.GetComponent<HeightCheck> ().targetHeight) {
				freeModeController.gameObject.GetComponent<FreeModeController> ().destructionMode = true;
				this.gameObject.SetActive (false);
			} else {
				StartCoroutine (showText (heightWarning));
			}
		} else {
			StartCoroutine (showText (lastPieceWarning));
		}
	}

	IEnumerator showText(GameObject textObj) {
		textObj.GetComponent<Text> ().enabled = true;
		yield return new WaitForSeconds (2.0f);
		textObj.GetComponent<Text> ().enabled = false;
	}
}
