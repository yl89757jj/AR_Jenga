using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {
	public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Change() {
		if (level == 1) {
			SceneManager.LoadScene ("FinalProject");
		}
		if (level == 2) {
			SceneManager.LoadScene ("FreeMode");
		}
	}
}
