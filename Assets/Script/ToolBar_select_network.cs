using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ToolBar_select_network : NetworkBehaviour {
    public Material select_material;
    private Material original_material;
    private GameObject selected_brick;
    public bool select_flag;

    void Start() {
        select_flag = false;
    }

    void Update(){
		if (!isLocalPlayer)
			return;
        if (Input.GetMouseButtonDown(0) && select_flag)
            StartCoroutine(ToolbarDeslect());
        if (Input.GetMouseButtonDown(1) && select_flag)
            selected_brick.transform.parent = null;
        if (Input.GetMouseButtonUp(1) && select_flag)
            selected_brick.transform.parent = transform;
    }

    void OnTriggerEnter(Collider other) {
		if (!isLocalPlayer)
			return;
        if (other.transform.parent.name == "Jenga"){
            select_flag = true;
            selected_brick = other.gameObject;
            original_material = selected_brick.GetComponent<Renderer>().material;
            other.transform.parent = transform;
            other.GetComponent<Renderer>().material = select_material;
            other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false; 
        }
    }

    IEnumerator ToolbarDeslect()
    {
        selected_brick.GetComponent<Renderer>().material = original_material;
        selected_brick.GetComponent<Collider>().attachedRigidbody.constraints = RigidbodyConstraints.None;
        selected_brick.transform.parent = GameObject.Find("Jenga").transform;
        selected_brick = null;
        original_material = null;
        yield return new WaitForSeconds(1f);
        select_flag = false;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }


}
