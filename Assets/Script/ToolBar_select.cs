using UnityEngine;
using System.Collections;

public class ToolBar_select : MonoBehaviour {
    public Material select_material;
    private Material original_material;
    private GameObject selected_brick;
    public bool select_flag;

    void Start() {
        select_flag = false;
    }

    void Update(){
        if (Input.GetMouseButtonDown(0) && select_flag)
            StartCoroutine(ToolbarDeslect());
        if (Input.GetMouseButtonDown(1))
            Debug.Log("To be determined");
    }

    void OnTriggerEnter(Collider other) {
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
