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
		if (newTurn == false) {
			newTurn = true;
			Debug.Log (jenga.transform.childCount);
			for (int i = 0; i < 10; i++) {
				int random = Mathf.RoundToInt (Random.value * 53);
				Debug.Log (random);
				Transform selectableBrick = jenga.transform.GetChild (random); 
				Selectable (selectableBrick.gameObject);
			}
			Debug.Log ("STARTS");
		}
	}

	void Selectable (GameObject brick){
		Debug.Log ("selectable"+brick);
		brick.GetComponent<Renderer>().material= brick.GetComponent<Brick> ().hightlight_material;
		brick.GetComponent<Brick> ().selectable = true;
	}

	public void EndTurn(){
		for (int i = 0; i < jenga.transform.childCount; i++) {
			Transform unSelectableBrick = jenga.transform.GetChild(i); 
			unSelectableBrick.GetComponent<Renderer>().material=unSelectableBrick.GetComponent<Brick> ().original_material;
			unSelectableBrick.GetComponent<Brick> ().selectable = false;
		}
		newTurn = false;
		Debug.Log ("End Turn");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
