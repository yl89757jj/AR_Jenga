using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndCheck : MonoBehaviour {
    public Text GameOver;
    public GameObject Toolbar;
    private GameObject brickselected = null;
	// Use this for initialization
	void Start () {
        GameOver.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        brickselected = Toolbar.GetComponent<ToolBar_select>().selected_brick;
   	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bricks")
        {
            if (!collision.gameObject.Equals(brickselected))
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    Vector3 offset = contact.point - transform.position;
                    Debug.Log(offset);
                    if (offset.magnitude > 3.5)
                        GameOver.enabled = true;
                }
            }
            else
                Debug.Log("Please Replace the brick");
        }
    }


}
