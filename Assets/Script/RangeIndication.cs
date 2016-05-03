using UnityEngine;
using System.Collections;

public class RangeIndication : MonoBehaviour {

	public bool showHint = false;
	private float delta = 0.05f;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Renderer> ().enabled = false;
		showHint = false;
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
			if (this.gameObject.GetComponent<Renderer> ().material.color.a > 0.2f) {
				delta = -0.01f;
			}
			if (this.gameObject.GetComponent<Renderer> ().material.color.a <0.01f) {
				delta = 0.01f;
			}
		} else {
			this.gameObject.GetComponent<Renderer> ().enabled = false;
		}
	}
}
