using UnityEngine;
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


	void Start() {
		gameController = GameObject.Find ("GameController");
		ground=GameObject.Find ("Ground");
		try_brick = null;
		select_flag = false;
		waitTime = 0f;
		selected_brick = null;


	}


	void Update() {
		if (!select_flag) {
			Indicator.GetComponent<RangeIndication> ().showHint = false;
		}
		if (select_flag) {
			/*Vector3 rotated_offset = new Vector3 ();
			rotated_offset.x = offset.x;
			rotated_offset.y = offset.y;
			rotated_offset.z = offset.z;
			Quaternion rot = selected_brick.transform.rotation;
			rotated_offset = rot * rotated_offset;
			Vector3 OFFSET = selected_brick.transform.position + rotated_offset - Indicator.transform.position;
			Vector3 RANGE = Indicator.transform.lossyScale;
			//if(OFFSET.x< RANGE.x && OFFSET.y<RANGE.y && OFFSET.z<RANGE.z)
			//	Indicator.GetComponent<RangeIndication> ().showHint = false;
			//else*/
				Indicator.GetComponent<RangeIndication> ().showHint = true;
		}
		newTurn = gameController.GetComponent<ButtonController> ().newTurn;
		Debug.Log ("New turn:" + newTurn);
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
			if (newTurn && selectable) {
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
		select_flag = true;
		selected_brick = try_brick;
		try_brick = null;
		if (selected_brick != null) {
			selected_brick.transform.parent = transform;
			selected_brick.GetComponent<Renderer> ().material = select_material;
			selected_brick.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false; 
			collisionPos = transform.position; //Jizhe add;
			gameController.SendMessage("EndTurn");
			Debug.Log ("Get new tern"+newTurn);
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
				yield return new WaitForSeconds (1f);
				select_flag = false;
				GetComponent<Renderer> ().enabled = true;
				GetComponent<Collider> ().enabled = true;
				newTurn = false;
			gameController.SendMessage ("EndTurn");
			gameController.GetComponent<GameStatus> ().inSelect = false;
			ground.SendMessage ("NextTurn");

			}

	}
}
