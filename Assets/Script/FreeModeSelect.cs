using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreeModeSelect : MonoBehaviour {
	public Material select_material;
	public GameObject selected_brick;
	private GameObject try_brick;
	public bool select_flag;
	public GameObject gameController;
	private float waitTime;

	void Start() {
		gameController = GameObject.Find ("GameController");
		select_flag = false;
		waitTime = 0f;
	}
		
	void Update() {
//		if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && select_flag)
			//StartCoroutine(ToolbarDeselect());
	}

	void OnTriggerStay(Collider other) {
		if (gameController.GetComponent<FreeModeController> ().gameover) {
			return;
		}
		if (other.gameObject.tag == "Bricks") {
			if (try_brick == null) {
				try_brick = other.gameObject;
				other.GetComponent<Renderer> ().material = other.GetComponent<Brick>().hightlight_material;
			} else if (!try_brick.Equals(other.gameObject)) {
				waitTime = 0f;
				try_brick.GetComponent<Renderer> ().material = try_brick.GetComponent<Brick> ().original_material;
				try_brick = other.gameObject;
				try_brick.GetComponent<Renderer> ().material = other.GetComponent<Brick>().hightlight_material;
				Debug.Log ("switch");
			} else {
				waitTime += Time.deltaTime;
			}

			if (waitTime > 2f && select_flag == false) {
				waitTime = 0;
				if (!gameController.GetComponent<FreeModeController> ().destructionMode) {
					SuspendSelect ();
				} else {
					select_flag = true;
					selected_brick = try_brick;
					try_brick = null;
					if (selected_brick) {
						Destroy (selected_brick);
						gameController.GetComponent<ScoreManager> ().score += 100;
					}
				}
			}
		}
	}

	//select after 2 seconds 
	void SuspendSelect(){
		select_flag = true;
		selected_brick = try_brick;
		try_brick = null;
		if (selected_brick != null) {
			selected_brick.transform.parent = transform;
			selected_brick.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			selected_brick.GetComponent<Renderer> ().material = select_material;
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false; 
			gameController.GetComponent<GameStatus> ().inSelect = true;
		}
	}
		
	IEnumerator ToolbarDeselect()
	{
		if (selected_brick != null) {
			selected_brick.GetComponent<Renderer> ().material = selected_brick.GetComponent<Brick>().original_material;
			selected_brick.GetComponent<Collider> ().attachedRigidbody.constraints = RigidbodyConstraints.None;
			selected_brick.GetComponent<Collider> ().attachedRigidbody.useGravity = true;
			selected_brick.GetComponent<Collider> ().attachedRigidbody.isKinematic = false;
			selected_brick.transform.parent = GameObject.Find ("Jenga").transform;
			selected_brick = null;
			yield return new WaitForSeconds (1f);
			select_flag = false;
			GetComponent<Renderer> ().enabled = true;
			GetComponent<Collider> ().enabled = true;
			gameController.GetComponent<GameStatus> ().inSelect = false;
		}
	}
}
