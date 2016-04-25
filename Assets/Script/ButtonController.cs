using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
	public bool newTurn;
//	private ArrayList selectableBricks= new ArrayList();
	private GameObject jenga;
	// Use this for initialization
	void Start () {
		newTurn = false;
		jenga= GameObject.Find ("Jenga");
	}

	//begin a new turen and randomly set the selectable bricks of player
	public void NewTurn(){
		newTurn = true;
		Debug.Log (jenga.transform.childCount);
		for (int i = 0; i < 10; i++) {
			int random = Mathf.RoundToInt(Random.value*53);
			Debug.Log (random);
			Transform selectableBrick = jenga.transform.GetChild(random); 
			Selectable (selectableBrick.gameObject);
		}
		Debug.Log ("STARTS");
	}

	void Selectable (GameObject brick){
		brick.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
		brick.GetComponent<Brick> ().selectable = true;
	}

	public void EndTurn(){
		for (int i = 0; i < jenga.transform.childCount; i++) {
			Transform unSelectableBrick = jenga.transform.GetChild(i); 
			unSelectableBrick.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
			unSelectableBrick.GetComponent<Brick> ().selectable = false;
		}
		newTurn = false;
		Debug.Log ("End Turn");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
