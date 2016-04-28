﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DownUp : MonoBehaviour {

	float pressTime = 0;
	bool buttonPressed =false;
	public GameObject[] selectBall;


	void Update(){
		if (buttonPressed == true) {
			pressTime += Time.deltaTime;	
			if (pressTime > 2)
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
			if (!GetComponent<GameStatus> ().inSelect) 
			{
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick)
					{
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = null;
						GetComponent<Button>().GetComponent<Image> ().color = Color.yellow;
					}
			} else 
			{
				foreach (GameObject p in selectBall)
					if (p.GetComponent<ToolBar_select> ().selected_brick) 
					{
						p.GetComponent<ToolBar_select> ().selected_brick.transform.parent = p.transform;
						GetComponent<Button>().GetComponent<Image> ().color = Color.white;
					}
			

			}
		}
		buttonPressed = false;
	}
		
}