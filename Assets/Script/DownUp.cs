using UnityEngine;
using System.Collections;


public class DownUp : MonoBehaviour {

	float pressTime = 0;
	bool buttonPressed =false;
	public GameObject[] selectBall;


	void Update(){
		if(buttonPressed == true)
			pressTime += Time.deltaTime;	
	}

	public void buttonDown(){
		pressTime = 0;
		buttonPressed = true;
	}

	public void buttonUp(){
		if (pressTime < 3)
			foreach (GameObject p in selectBall)
				p.SendMessage ("ToolBar_deselect");
		else {
			if (!GetComponent<GameStatus> ().inSelect) {
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick) {
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = null;
						GetComponent<GameStatus> ().inSelect = true;
					}
			} else {
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick) {
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = p.transform;
						GetComponent<GameStatus> ().inSelect = false;
					}
			
			}


		}
	}
}
