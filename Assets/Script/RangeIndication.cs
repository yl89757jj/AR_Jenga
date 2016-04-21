﻿using UnityEngine;
using System.Collections;

public class RangeIndication : MonoBehaviour {

	public bool showHint = false;
	private float delta = 0.05f;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.H)) {
			showHint = !showHint;
		}
		if (showHint) {
			this.gameObject.GetComponent<Renderer> ().enabled = true;
			Material mat = this.gameObject.GetComponent<Renderer> ().material;
			Color newColor = mat.color;
			newColor.a += delta;
			this.gameObject.GetComponent<Renderer> ().material.color = newColor;
			if (this.gameObject.GetComponent<Renderer> ().material.color.a > 0.95f) {
				delta = -0.05f;
			}
			if (this.gameObject.GetComponent<Renderer> ().material.color.a <0.05f) {
				delta = 0.05f;
			}
		} else {
			this.gameObject.GetComponent<Renderer> ().enabled = false;
		}
	}
}
