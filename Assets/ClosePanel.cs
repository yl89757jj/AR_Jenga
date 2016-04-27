using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClosePanel : MonoBehaviour {
	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close() {
		panel.gameObject.SetActive (false);
	}
}
