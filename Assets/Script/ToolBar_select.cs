using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolBar_select : MonoBehaviour {
	public Material select_material;
	public Material transparent_mat;
	public bool select_flag;
	public GameObject selected_brick;
	private Material original_material;
	private GameObject[] brickObj;
	private List<GameObject> highlighted;
    public GameObject JengaGame;
    public Camera ARcamera;
	public Vector3 collisionPos; //Jizhe add.
	public GameObject gameController;

    void Start() {
        select_flag = false;
		highlighted = new List<GameObject>();
    }

    void Update(){

        if (!select_flag && Input.GetMouseButton(0) && GetComponent<Renderer>().enabled)
        {
            //StartCoroutine(ChangeLevel());
        }
		if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && select_flag)
            ToolbarDeslect();
        if (Input.GetMouseButtonDown(1) && select_flag)
            selected_brick.transform.parent = null;
        if (Input.GetMouseButtonUp(1) && select_flag)
            selected_brick.transform.parent = transform;
    }

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Bricks"){
			collisionPos = this.gameObject.transform.position; //Jizhe add;
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
			gameController.GetComponent<GameStatus> ().inSelect = true;
        }
		if (other.gameObject.tag == "Selector") {
			GameObject.Find ("GameController").GetComponent<GameStart> ().RestartPlaymode ();
		}
		if (other.gameObject.tag == "Virtual Stick") {
			GameObject.Find ("GameController").GetComponent<GameStart> ().RestartFreemode();
		}
    }

	private void ToolbarDeslect()
    {
        selected_brick.GetComponent<Renderer> ().material = original_material;
		selected_brick.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		selected_brick.GetComponent<Rigidbody> ().isKinematic = false;
		selected_brick.GetComponent<Rigidbody> ().useGravity = true;
        selected_brick.transform.parent = GameObject.Find("Jenga").transform;
		reHighlight (original_material);
        selected_brick = null;
        original_material = null;
		gameController.GetComponent<GameStatus> ().inSelect = false;
		StartCoroutine(Wait());
//        select_flag = false;
//        GetComponent<Renderer>().enabled = true;
//        GetComponent<Collider>().enabled = true;
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
		select_flag = false;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider>().enabled = true;
	}

    IEnumerator ChangeLevel()
    {
        Debug.Log("ChangeLevel");
        GameObject Jenga = GameObject.Find("Jenga");
        Collider[] colliderComponents = Jenga.GetComponentsInChildren<Collider>(true);
        /*
        foreach (Collider component in colliderComponents)
        {
            if (component.attachedRigidbody)
                component.attachedRigidbody.isKinematic = true;
        }
        */
        Vector3 last_toolbar_screen = ARcamera.WorldToScreenPoint(transform.position);
        yield return new WaitForSeconds(0.05f);
        Vector3 current_toolbar_screen = ARcamera.WorldToScreenPoint(transform.position);
        Vector3 jenga_position = JengaGame.transform.localPosition;
        jenga_position.y += (current_toolbar_screen.y - last_toolbar_screen.y) / 1000;
        Debug.Log(jenga_position.y);
        JengaGame.transform.localPosition = jenga_position;
        /*
        foreach (Collider component in colliderComponents)
        {
            if (component.attachedRigidbody)
                component.attachedRigidbody.isKinematic = false;
        }
        */
    }
}
