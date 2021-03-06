﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolBar_select : MonoBehaviour {
	public Material select_material;
	public GameObject selected_brick;
	public GameObject try_brick;
	public bool select_flag;
	public bool newTurn;
	private int random;
	public Vector3 collisionPos; //Jizhe add.
	private GameObject gameController;
	public GameObject Indicator;
	public float waitTime;
	private GameObject ground;
	private Vector3 offset = new Vector3(2.287587f, 0.377825f, 0.7604125f);
	private AudioSource audio_s;


	void Start() {
		gameController = GameObject.Find ("GameController");
		audio_s = GetComponent<AudioSource> ();
		ground=GameObject.Find ("Ground");
		try_brick = null;
		select_flag = false;
		waitTime = 0f;
		selected_brick = null;


	}


	void Update() {
		if (select_flag) {
			Vector3 rotated_offset = new Vector3 ();
			rotated_offset.x = offset.x;
			rotated_offset.y = offset.y;
			rotated_offset.z = offset.z;
			Quaternion rot = selected_brick.transform.rotation;
			rotated_offset = rot * rotated_offset;
			Vector3 localP = Indicator.transform.InverseTransformPoint(selected_brick.transform.position + rotated_offset);
			Vector3 range = Indicator.transform.localScale;
			Debug.Log (localP);
			Debug.Log (range);
			if (Mathf.Abs(localP.x/range.x) < 1.0 && Mathf.Abs(localP.y/range.y) < 1.0 && Mathf.Abs(localP.z/range.z) < 1.0)
				Indicator.GetComponent<RangeIndication> ().showHint = false;
			else
				Indicator.GetComponent<RangeIndication> ().showHint = true;
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Selector") {
			gameController.GetComponent<GameStart> ().RestartPlaymode ();
		}
		if (other.gameObject.tag == "Virtual Stick") {
			gameController.GetComponent<GameStart> ().RestartFreemode ();
		}
	}

	void OnTriggerStay(Collider other) {
		Debug.Log (other);
		if (other.gameObject.tag == "Bricks") {
			bool selectable = other.GetComponent<Brick> ().selectable;
			if (selectable) {
				if (try_brick==null) {
					waitTime = 0f;
					try_brick = other.gameObject;
					try_brick.GetComponent<Renderer> ().material=other.GetComponent<Brick> ().tried_material;
				} else if (try_brick.Equals(other.gameObject)) {
					waitTime += Time.deltaTime;
				} else{
					waitTime = 0f;
					try_brick.GetComponent<Renderer> ().material=try_brick.GetComponent<Brick> ().hightlight_material;
					try_brick = other.gameObject;
					try_brick.GetComponent<Renderer> ().material=try_brick.GetComponent<Brick> ().tried_material;
				}

				if (waitTime > 2f && select_flag == false) {
					waitTime = 0;
					SuspendSelect ();
				}
			}
		}
	}
		

	//select after 5 seconds 
	void SuspendSelect(){
		audio_s.Play ();
		select_flag = true;
		selected_brick = try_brick;
		try_brick = null;
		if (selected_brick != null) {
			selected_brick.transform.parent = transform;
			selected_brick.GetComponent<Renderer> ().material = select_material;
			selected_brick.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			selected_brick.GetComponent<Brick> ().selected = true;
			Debug.Log ("Brick"+selected_brick.GetComponent<Brick> ().selected);
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false; 
			collisionPos = transform.position; //Jizhe add;
			gameController.SendMessage("EndTurn");
			Indicator.GetComponent<RangeIndication> ().showHint = true;
			//selected_brick.GetComponent<Brick> ().selectable = true;
		}
	}
		


	private void ToolBar_deselect(){
		StartCoroutine (ToolbarDeselect());
	}


	IEnumerator ToolbarDeselect()
	{		
			if (selected_brick != null) {
				selected_brick.GetComponent<Renderer> ().material = selected_brick.GetComponent<Brick> ().original_material;
				selected_brick.GetComponent<Collider> ().attachedRigidbody.constraints = RigidbodyConstraints.None;
				selected_brick.transform.parent = GameObject.Find ("Jenga").transform;
			    audio_s.Play ();
				yield return new WaitForSeconds (1f);
				select_flag = false;
				GetComponent<Renderer> ().enabled = true;
				GetComponent<Collider> ().enabled = true;
				newTurn = false;
			Indicator.GetComponent<RangeIndication> ().showHint = false;
			gameController.SendMessage ("EndTurn");
			gameController.GetComponent<GameStatus> ().inSelect = false;
			ground.SendMessage ("NextTurn");

			}

	}
}
