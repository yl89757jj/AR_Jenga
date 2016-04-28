using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {
	private Vector3 offset = new Vector3(2.287587f, 0.377825f, 0.7604125f);
	private Vector3 origin;
	public GameObject xaxis;
	public GameObject yaxis;
	public GameObject zaxis;
	// Use this for initialization
	void Start () {
		xaxis = GameObject.Find ("Xaxis");
		yaxis = GameObject.Find ("Yaxis");
		zaxis = GameObject.Find ("Zaxis");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.tag == "Bricks" && this.gameObject.transform.parent != null 
			&& this.gameObject.transform.parent.gameObject.name == "Select") {
			hideCollision ();
			Vector3 rotated_offset = new Vector3 ();
			rotated_offset.x = offset.x;
			rotated_offset.y = offset.y;
			rotated_offset.z = offset.z;
			Quaternion rot = this.transform.rotation;
			rotated_offset = rot * rotated_offset;
			origin = this.gameObject.transform.position + rotated_offset;
			drawCollision ();
		} else if (this.gameObject.name == "Select" && this.gameObject.transform.childCount == 0 
			&& this.gameObject.GetComponent<Renderer>().enabled) {
			hideCollision ();
			Vector3 rotated_offset = new Vector3 ();
			Quaternion rot = this.transform.rotation;
			origin = this.gameObject.transform.position;
			drawCollision ();
		}
	}

	private void drawCollision() {
		xaxis.GetComponent<LineRenderer> ().enabled = true;
		yaxis.GetComponent<LineRenderer> ().enabled = true;
		zaxis.GetComponent<LineRenderer> ().enabled = true;
		Vector3 downEnd = findCollisionPoint (-Vector3.up);
		Vector3[] ypos = new Vector3[2];
		ypos [0] = origin;
		ypos [1] = downEnd;
		yaxis.GetComponent<LineRenderer> ().SetPositions (ypos);
		Vector3 leftEnd = findCollisionPoint (Vector3.left);
		Vector3 rightEnd = findCollisionPoint (-Vector3.left);
		Vector3[] xpos = new Vector3[2];
		xpos [0] = leftEnd;
		xpos [1] = rightEnd;
		xaxis.GetComponent<LineRenderer> ().SetPositions (xpos);
		Vector3 frontEnd = findCollisionPoint (Vector3.forward);
		Vector3 backEnd = findCollisionPoint (-Vector3.forward);
		Vector3[] zpos = new Vector3[2];
		zpos [0] = frontEnd;
		zpos [1] = backEnd;
		zaxis.GetComponent<LineRenderer> ().SetPositions (zpos);
	}

	private Vector3 findCollisionPoint(Vector3 dir) {
		Ray ray = new Ray ();
		ray.origin = origin;
		ray.direction = dir;
		RaycastHit hit;
		Vector3 end = new Vector3();
		if (Physics.Raycast (ray, out hit)) {
			end = hit.point;
			Debug.DrawLine (origin, end, Color.green, 0.0f, true);
		}
		return end;
	}

	private void hideCollision() {
		xaxis.GetComponent<LineRenderer> ().enabled = false;
		yaxis.GetComponent<LineRenderer> ().enabled = false;
		zaxis.GetComponent<LineRenderer> ().enabled = false;
	}
}
