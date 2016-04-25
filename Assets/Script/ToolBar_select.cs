﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolBar_select : MonoBehaviour {
	public Material select_material;
	private Material original_material;
	public GameObject selected_brick;
	private GameObject try_brick;
	public bool select_flag;
	private bool newTurn;
	private int random;
	public Vector3 collisionPos; //Jizhe add.
	private GameObject newTurnButton;
	public GameObject gameController;
	private float waitTime;

	void Start() {
		gameController = GameObject.Find ("GameController");
		newTurnButton = GameObject.Find ("NewTurn");
		Debug.Log (newTurnButton);
		select_flag = false;
		waitTime = 0f;
	
	}


	void Update(){
		Debug.Log ("wait"+ waitTime);
		newTurn = newTurnButton.GetComponent<ButtonController> ().newTurn;
		if (Input.GetMouseButtonDown(0) && select_flag)
			StartCoroutine(ToolbarDeslect());
		if (Input.GetMouseButtonDown(1) && select_flag)
			selected_brick.transform.parent = null;
		if (Input.GetMouseButtonUp(1) && select_flag)
			selected_brick.transform.parent = transform;
	}


	void OnTriggerStay(Collider other) {
		bool selectable = other.GetComponent<Brick> ().selectable;
		Debug.Log ("NEW TURN  "+newTurn);
		Debug.Log("SELECTABLE   "+selectable);
		if (newTurn&&selectable) {
			if (try_brick==null) {
				try_brick = other.gameObject;
				original_material = other.GetComponent<Renderer> ().material;
				other.GetComponent<Renderer> ().material.shader = Shader.Find ("Outlined/Silhouette Only");
			}else if (try_brick!=other.gameObject) {
				waitTime = 0f;
				try_brick = null;
				try_brick.GetComponent<Renderer> ().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
				Debug.Log ("switch");
			}
			else{
				waitTime += Time.deltaTime;}

			if (waitTime > 5f&&select_flag == false)
					SuspendSelect ();
			}

	}
		

	//select after 5 seconds 
	void SuspendSelect(){
		select_flag = true;
		selected_brick = try_brick;
		try_brick = null;
		if (selected_brick != null) {
			selected_brick.transform.parent = transform;
			selected_brick.GetComponent<Renderer> ().material = select_material;
			selected_brick.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false; 
			Debug.Log ("get");
			collisionPos = transform.position; //Jizhe add;
			gameController.GetComponent<GameStatus> ().inSelect = true;
		}
	}


	IEnumerator ToolbarDeslect()
	{
		selected_brick.GetComponent<Renderer>().material = original_material;
		selected_brick.GetComponent<Renderer>().material.shader=Shader.Find("Standard");
		selected_brick.GetComponent<Collider>().attachedRigidbody.constraints = RigidbodyConstraints.None;
		selected_brick.transform.parent = GameObject.Find("Jenga").transform;
		selected_brick = null;
		original_material = null;
		yield return new WaitForSeconds(1f);
		select_flag = false;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
		newTurn = false;
		newTurnButton.SendMessage ("EndTurn");
		newTurnButton.SendMessage ("NewTurn");
		gameController.GetComponent<GameStatus> ().inSelect = false;
	}
}
