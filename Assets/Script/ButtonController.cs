﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
	public bool newTurn;
//	private ArrayList selectableBricks= new ArrayList();
	private GameObject jenga;
	private float timer = -1;
	public Text message;
	// Use this for initialization
	void Start () {
		newTurn = false;
		jenga= GameObject.Find ("Jenga");
	}

	//begin a new turen and randomly set the selectable bricks of player
	public void NewTurn(){
		if (newTurn == false) {
			timer = 0;
			message.enabled = true;
			newTurn = true;
			foreach (Transform brick in jenga.transform){
				brick.gameObject.GetComponent<Brick> ().selected=false;
			}
			for (int i = 0; i < 10; i++) {
				int random = Mathf.RoundToInt (Random.value * 53);
				Transform selectableBrick = jenga.transform.GetChild (random); 
				Selectable (selectableBrick.gameObject);
			}
		}
	}

	void Selectable (GameObject brick){
		brick.GetComponent<Renderer>().material= brick.GetComponent<Brick> ().hightlight_material;
		brick.GetComponent<Brick> ().selectable = true;
	}

	public void EndTurn(){
		for (int i = 0; i < jenga.transform.childCount; i++) {
			Transform unSelectableBrick = jenga.transform.GetChild(i); 
			unSelectableBrick.GetComponent<Renderer>().material=unSelectableBrick.GetComponent<Brick> ().original_material;
			unSelectableBrick.GetComponent<Brick> ().selectable = false;
		}
		newTurn = false;
	}

	// Update is called once per frame
	void Update () {
		if (timer < 2.5 && timer >= 0)
			timer += Time.deltaTime;
		else {
			timer = -1;
			message.enabled = false;
		}
		
	}
}
