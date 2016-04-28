using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishConstruction : MonoBehaviour {
	public GameObject freeModeController;
	public GameObject heightWarning;
	public GameObject lastPieceWarning;
	public GameObject destructionHelp;
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
				GameObject.Find ("SpawnRegion").SetActive (false);
				StartCoroutine (showText (destructionHelp, 5.0f));
				this.gameObject.SetActive (false);
			} else {
				StartCoroutine (showText (heightWarning, 2.0f));
			}
		} else {
			StartCoroutine (showText (lastPieceWarning, 2.0f));
		}
	}

	IEnumerator showText(GameObject textObj, float time) {
		textObj.GetComponent<Text> ().enabled = true;
		yield return new WaitForSeconds (time);
		textObj.GetComponent<Text> ().enabled = false;
	}
}
