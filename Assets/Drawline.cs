using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {
	public bool showCollision = false;
	private Vector3 offset = new Vector3(2.287587f, 0, 0.7604125f);
	private Vector3 origin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (showCollision) {
			origin = this.gameObject.transform.position + offset;
			drawCollision (-Vector3.up);
			drawCollision (Vector3.left);
			drawCollision (Vector3.forward);
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
