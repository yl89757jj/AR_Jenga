using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {
	private Vector3 offset = new Vector3(2.287587f, 0.377825f, 0.7604125f);
	private Vector3 origin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.gameObject.name == "Select") {
			Vector3 rotated_offset = new Vector3 ();
			rotated_offset.x = offset.x;
			rotated_offset.y = offset.y;
			rotated_offset.z = offset.z;
			Quaternion rot = this.transform.rotation;
			rotated_offset = rot * rotated_offset;
			origin = this.gameObject.transform.position + rotated_offset;
			drawCollision (-Vector3.up);
			drawCollision (Vector3.left);
			drawCollision (Vector3.forward);
			drawCollision (-Vector3.left);
			drawCollision (-Vector3.forward);
		}
		if (this.gameObject.name == "Select" && this.gameObject.transform.childCount == 0) {
			Vector3 rotated_offset = new Vector3 ();
			Quaternion rot = this.transform.rotation;
			origin = this.gameObject.transform.position;
			drawCollision (-Vector3.up);
			drawCollision (Vector3.left);
			drawCollision (Vector3.forward);
			drawCollision (-Vector3.left);
			drawCollision (-Vector3.forward);
		}
	}

	private void drawCollision(Vector3 dir) {
		Ray downRay = new Ray ();
		downRay.origin = origin;
		downRay.direction = dir;
		RaycastHit downHit;
		Vector3 downEnd = new Vector3();
		if (Physics.Raycast (downRay, out downHit)) {
			downEnd = downHit.point;
			Debug.DrawLine (origin, downEnd, Color.green, 0.0f, true);
		}
	}
}
