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
	public bool gameover = false;
	public GameObject postGamePanel;
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
			if (this.gameObject.GetComponent<HeightCheck> ().currentHeight
				< this.gameObject.GetComponent<HeightCheck> ().targetHeight/2) {
				//Game over
				this.gameObject.GetComponent<ScoreManager>().getResult();
				gameover = true;
				int totalScore = this.gameObject.GetComponent<ScoreManager> ().score;
				this.gameObject.GetComponent<ScoreManager> ().scoreBoard.GetComponent<Text> ().enabled = false;
				postGamePanel.SetActive (true);
				postGamePanel.GetComponentInChildren<Text> ().text = "Game over\nYour final score is:\n" + totalScore.ToString ();
			}
		}
	}
}
