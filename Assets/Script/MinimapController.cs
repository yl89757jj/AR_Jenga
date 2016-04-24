using UnityEngine;
using System.Collections;

public class MinimapController : MonoBehaviour {
	public bool frontal_mode = false;
	private Vector3 overhead;
	private Quaternion overhead_rot;
	private Vector3 frontal;
	private Quaternion frontal_rot;
	// Use this for initialization
	void Start () {
		overhead = new Vector3 (0, 90f, 0);
		overhead_rot = Quaternion.Euler (new Vector3 (90f, 0, 0));
		frontal = new Vector3 (0, 20f, -30f);
		frontal_rot = Quaternion.Euler (new Vector3 (0, 0, 0));
		this.gameObject.transform.position = overhead;
		this.gameObject.transform.rotation = overhead_rot;
	}
	
	// Update is called once per frame
	void Update () {
		if (!frontal_mode) {
			this.gameObject.transform.position = overhead;
			this.gameObject.transform.rotation = overhead_rot;
		}
		if (frontal_mode) {
			this.gameObject.transform.position = frontal;
			this.gameObject.transform.rotation = frontal_rot;
		}
	}
}
