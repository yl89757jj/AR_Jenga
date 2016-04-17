using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolBar_select : MonoBehaviour {
	public Material select_material;
	public Material transparent_mat;
	public bool select_flag;
	private Material original_material;
	private GameObject selected_brick;
	private GameObject[] brickObj;
	private List<GameObject> highlighted;

    void Start() {
        select_flag = false;
		highlighted = new List<GameObject>();
    }

    void Update(){
        if (Input.GetMouseButtonDown(0) && select_flag)
            ToolbarDeslect();
        if (Input.GetMouseButtonDown(1) && select_flag)
            selected_brick.transform.parent = null;
        if (Input.GetMouseButtonUp(1) && select_flag)
            selected_brick.transform.parent = transform;
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.parent.name == "Jenga"){
            select_flag = true;
            selected_brick = other.gameObject;
			brickObj = GameObject.FindGameObjectsWithTag ("Bricks");
			highlightObj (brickObj, selected_brick);
            original_material = selected_brick.GetComponent<Renderer>().material;
            other.transform.parent = transform;
            other.GetComponent<Renderer>().material = select_material;
            other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false; 
        }
		if (other.gameObject.tag == "Selector") {
			GameObject.Find ("GameController").GetComponent<GameStart> ().RestartGame ();
		}
    }

	private void ToolbarDeslect()
    {
        selected_brick.GetComponent<Renderer>().material = original_material;
		selected_brick.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        selected_brick.transform.parent = GameObject.Find("Jenga").transform;
		reHighlight (original_material);
        selected_brick = null;
        original_material = null;
		StartCoroutine(Wait());
        select_flag = false;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

	public void highlightObj (GameObject[] bricks, GameObject selected_obj)
	{
		Debug.Log ("transparent");
		highlighted.Clear ();
		foreach (GameObject item in bricks) {
			if (item.Equals (selected_obj))
				continue;
			if (Mathf.Abs (item.transform.position.y - selected_obj.transform.position.y) < 0.5f && Mathf.Abs (item.transform.position.z - selected_obj.transform.position.z) < 1f) {
				item.GetComponent<Renderer> ().material = transparent_mat;
				highlighted.Add (item);
			}
		}
	}

	public void reHighlight (Material orig_mat)
	{
		foreach (GameObject item in highlighted) {
			item.GetComponent<Renderer> ().material = orig_mat;
		}
		highlighted.Clear ();
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(1.0f);
	}
}
