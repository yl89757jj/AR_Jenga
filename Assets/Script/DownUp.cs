using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DownUp : MonoBehaviour {

	float pressTime = 0;
	bool buttonPressed = false;
	int status = 0;
	public GameObject[] selectBall;


	void Update(){
		if (buttonPressed == true) {
			pressTime += Time.deltaTime;	
			if (pressTime > 2 && status == 0)
				foreach (GameObject p in selectBall) { 
					p.SendMessage ("ToolBar_deselect");
					buttonPressed = false;
				}
		}
	}

	public void buttonDown(){
		pressTime = 0;
		buttonPressed = true;
	}

	public void buttonUp(){
		if (pressTime < 1)
		{
			if (status == 0) {
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick) {
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = null;
						GameObject.Find ("Button").GetComponent<Image> ().color = Color.gray;
						status = 1;
					}
			}
			else{
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick){
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = p.transform;
						GameObject.Find("Button").GetComponent<Image> ().color = Color.white;
						status = 0;
					}
			}
		}
		buttonPressed = false;
	}
		
}
