using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 prev_pos = this.gameObject.transform.position;
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.gameObject.transform.Rotate (new Vector3 (5f, 0, 0));
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			this.gameObject.transform.Rotate (new Vector3 (-5f, 0, 0));
		}
		if (Input.GetKey (KeyCode.A)) {
			this.gameObject.transform.Translate (Vector3.left);
		}
		if (Input.GetKey (KeyCode.D)) {
			this.gameObject.transform.Translate (Vector3.right);
		}
		if (Input.GetKey (KeyCode.W)) {
			this.gameObject.transform.Translate (Vector3.forward);
		}
		if (Input.GetKey (KeyCode.S)) {
			this.gameObject.transform.Translate (new Vector3(0, 0, -1.0f));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.gameObject.transform.Rotate (new Vector3 (0, -5f, 0));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.gameObject.transform.Rotate (new Vector3 (0, 5f, 0));
		}
		if (this.gameObject.transform.position.y > 39f 
			|| this.gameObject.transform.position.y < 0 || this.gameObject.transform.position.x < -35f
			|| this.gameObject.transform.position.x > 35f || this.gameObject.transform.position.z < -35f
			|| this.gameObject.transform.position.z > 35f) {
			this.gameObject.transform.position = prev_pos;
		}
	}
}
