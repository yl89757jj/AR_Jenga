using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

	public bool inSelect = false;
	public bool inRotation = false;
	public bool movingCamera = false;
	public GameObject workSpace;
	private Vector2 touchOrigin = -Vector2.one;
	public GameObject minimapCam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!inSelect) {
			if (Input.touchCount == 1) {
				Touch myTouch = Input.GetTouch (0);
				if (myTouch.phase == TouchPhase.Began) {
					touchOrigin = myTouch.position;
					GameObject bricks = workSpace.gameObject.transform.FindChild ("Jenga").gameObject;
					foreach (Transform tf in bricks.transform) {
						Rigidbody rb = tf.gameObject.GetComponent<Rigidbody> ();
						Destroy (rb);
					}
				} else if (myTouch.phase == TouchPhase.Moved) {
					float deltaX = myTouch.position.x - touchOrigin.x;
					float deltaY = myTouch.position.y - touchOrigin.y;
					if (deltaX > 40f) {
						workSpace.gameObject.transform.RotateAround (workSpace.gameObject.transform.position, Vector3.up, -3f);
						minimapCam.gameObject.transform.RotateAround (new Vector3 (0, 0, 0), Vector3.up, -3f);
					}
					if (deltaX < -40f) {
						workSpace.gameObject.transform.RotateAround (workSpace.gameObject.transform.position, Vector3.up, 3f);
						minimapCam.gameObject.transform.RotateAround (new Vector3 (0, 0, 0), Vector3.up, 3f);
					}
					Vector3 originPos = workSpace.gameObject.transform.position;
					if (deltaY > 40f) {
						workSpace.gameObject.transform.Translate (new Vector3 (0, 0.5f, 0));
					}
					if (deltaY < -40f) {
						workSpace.gameObject.transform.Translate (new Vector3 (0, -0.5f, 0));
					}
					if (workSpace.gameObject.transform.position.y > 10f || workSpace.gameObject.transform.position.y < -30f) {
						workSpace.gameObject.transform.position = originPos;
					}
				} else {
					GameObject bricks = workSpace.gameObject.transform.FindChild ("Jenga").gameObject;
					foreach (Transform tf in bricks.transform) {
						Rigidbody rb = tf.gameObject.AddComponent<Rigidbody> ();
						rb.mass = 1;
						rb.useGravity = true;
						rb.isKinematic = false;
						rb.constraints = new RigidbodyConstraints ();
					}
				}
			} else {
				if (Input.GetKey (KeyCode.A)) {
					workSpace.gameObject.transform.RotateAround (workSpace.gameObject.transform.position, Vector3.up, -3f);
					minimapCam.gameObject.transform.RotateAround (new Vector3 (0, 0, 0), Vector3.up, -3f);
				}
				if (Input.GetKey (KeyCode.D)) {
					workSpace.gameObject.transform.RotateAround (workSpace.gameObject.transform.position, Vector3.up, 3f);
					minimapCam.gameObject.transform.RotateAround (new Vector3 (0, 0, 0), Vector3.up, 3f);
				}
				Vector3 originPos = workSpace.gameObject.transform.position;
				if (Input.GetKey (KeyCode.W)) {
					workSpace.gameObject.transform.Translate (new Vector3 (0, 0.5f, 0));
				}
				if (Input.GetKey (KeyCode.S)) {
					workSpace.gameObject.transform.Translate (new Vector3 (0, -0.5f, 0));
				}
				if (workSpace.gameObject.transform.position.y > 10f || workSpace.gameObject.transform.position.y < -30f) {
					workSpace.gameObject.transform.position = originPos;
				}
			}
		}
	}
}
