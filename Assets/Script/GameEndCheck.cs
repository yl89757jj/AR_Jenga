using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndCheck : MonoBehaviour {
    public GameObject[] bottomBricks;
    public Text GameOver;
	// Use this for initialization
	void Start () {
        GameOver.enabled = false;
        foreach (GameObject bb in bottomBricks)
            bb.layer = 11;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.layer != 11) {
			Debug.Log ("GameOver!");
			GameOver.enabled = true;
		}
    }
}
