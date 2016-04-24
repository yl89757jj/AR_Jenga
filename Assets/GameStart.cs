﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	public bool gameStarted = false;
	public GameObject startText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		startText.SetActive (!gameStarted);
	}

	public void RestartPlaymode() {
		SceneManager.LoadScene ("FinalProject");
	}

	public void RestartFreemode() {
		SceneManager.LoadScene ("FreeMode");
	}
}
