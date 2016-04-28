using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FreeModeController : MonoBehaviour {
	public int numberOfBricks;
	public GameObject brickCountText;
	public GameObject confirmButton;
	public Color normalColor;
	public Color lastColor;
	public bool destructionMode = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!destructionMode) {
			brickCountText.GetComponent<Text> ().text = numberOfBricks.ToString ();
			if (numberOfBricks <= 10) {
				brickCountText.GetComponent<Text> ().color = lastColor;
			} else {
				brickCountText.GetComponent<Text> ().color = normalColor;
			}
			if (numberOfBricks == 0) {
				confirmButton.SetActive (true);
			}
		} else {
			brickCountText.transform.parent.gameObject.GetComponent<Text> ().enabled = false;
			brickCountText.GetComponent<Text> ().enabled = false;
		}
	}
}
