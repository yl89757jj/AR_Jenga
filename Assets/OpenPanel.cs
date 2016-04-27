using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenPanel : MonoBehaviour {
	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowPanel() {
		panel.gameObject.SetActive (true);
	}
}
