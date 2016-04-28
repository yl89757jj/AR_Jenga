using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public bool selectable;
	public Material original_material;
	public Material hightlight_material;
	public Material tried_material;

	// Use this for initialization
	void Start () {
		selectable = false;
		original_material = transform.GetComponent<Renderer> ().material;

		if (original_material.name.Equals ("Jenga_Red_wood.mat")) {
			hightlight_material = Resources.Load ("Materials/Jenga_Red_wood2.mat")as Material;
			tried_material = Resources.Load ("Materials/Jenga_Red_wood1.mat")as Material;
		} else {
			hightlight_material=Resources.Load ("Materials/jenga_Wood_Floor_Light2.mat")as Material;
			tried_material=Resources.Load ("Materials/jenga_Wood_Floor_Light1.mat")as Material;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
