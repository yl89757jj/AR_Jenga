using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void instructionShow()
	{
		this.gameObject.SetActive (true);
	}

	public void instructionHide ()
	{
		this.gameObject.SetActive (false);
	}
}
