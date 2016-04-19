using UnityEngine;
using System.Collections;

public class minimap2Controller : MonoBehaviour {

	public ToolBar_select toolbarscript;
	public GameObject mainCamera;
	public GameObject selectBrick;
	private float xdelta = 5.0f;
	private float ydelta = 0.0f;
	private float zdelta = 4.0f;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		toolbarscript = GameObject.Find ("Select4").GetComponent<ToolBar_select> ();
		//this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (toolbarscript.selected_brick != null) {
			Debug.Log (toolbarscript.selected_brick.name);
			//this.gameObject.SetActive (true);
			selectBrick = toolbarscript.selected_brick;
			//this.gameObject.transform.position -= mainCamera.transform.position - selectBrick.transform.position;
			Vector3 vec_delta = toolbarscript.collisionPos-selectBrick.transform.position;
			float x_ = Mathf.Max (vec_delta.x, -vec_delta.x);
			float y_ = Mathf.Max (vec_delta.y, -vec_delta.y);
			float z_ = Mathf.Max (vec_delta.z, -vec_delta.z);
			int max_axis = -1;
			float max_dis = 0f;
			if (x_ > y_) {
				max_dis = x_;
				max_axis = 1;
			} else {
				max_dis = y_;
				max_axis = 2;
			}
			if (z_ > max_dis) {
				max_dis = z_;
				max_axis = 3;
			}

			if (max_axis == 3) {
				float en = vec_delta.z / Mathf.Abs (vec_delta.z);
				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x + xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z + en * zdelta);
				this.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			}
			if (max_axis == 2) {
				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x, selectBrick.transform.position.y + 2.0f, selectBrick.transform.position.z);
				this.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (90.0f, 0.0f, 0.0f));
			}
			if (max_axis == 1) {
				float en = vec_delta.x / Mathf.Abs (vec_delta.x);
				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x + en * xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z + zdelta);
				this.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			}

//			if (mainCamera.transform.position.x - selectBrick.transform.position.x < 0
//			    && mainCamera.transform.position.z - selectBrick.transform.position.z < 0) {
//				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x + xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z + zdelta);
//			} 
//			if (mainCamera.transform.position.x - selectBrick.transform.position.x < 0
//			    && mainCamera.transform.position.z - selectBrick.transform.position.z > 0) {
//				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x + xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z - zdelta);
//			} 
//			if (mainCamera.transform.position.x - selectBrick.transform.position.x > 0
//			    && mainCamera.transform.position.z - selectBrick.transform.position.z < 0) {
//				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x - xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z + zdelta);
//			} 
//			if (mainCamera.transform.position.x - selectBrick.transform.position.x > 0
//			    && mainCamera.transform.position.z - selectBrick.transform.position.z > 0) {
//				this.gameObject.transform.position = new Vector3 (selectBrick.transform.position.x - xdelta, selectBrick.transform.position.y + ydelta, selectBrick.transform.position.z - zdelta);
//			} 
		} 
//		else {
//			this.gameObject.SetActive (false);
//		}
	}
}
