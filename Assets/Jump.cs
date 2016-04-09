using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public bool up = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (up) {
			this.transform.position = this.transform.position + new Vector3 (0, 0.1f, 0);
			if (this.transform.position.y > 5.0f) {
				up = false;
			}
		} else {
			this.transform.position = this.transform.position - new Vector3 (0, 0.1f, 0);
			if (this.transform.position.y < 1.0f) {
				up = true;
			}
		}
	}
}
