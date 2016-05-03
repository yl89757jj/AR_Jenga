using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndCheck : MonoBehaviour {
    public Text GameOver;
 //   public GameObject Toolbar;
    private GameObject brickselected = null;
	private bool collide_flag;
	public GameObject gameController;
	private float next_turn_wait;
	private bool nextTurn;
	public Text message;
	private float timer;

	// Use this for initialization
	void Start () {
        GameOver.enabled = false;
		collide_flag = false;
		gameController = GameObject.Find("GameController");
		next_turn_wait = 0f;
		nextTurn = false;
		brickselected = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 0 && timer < 2.5)
			timer += Time.deltaTime;
		else {
			timer = -1;
			message.enabled = false;
		}
		if (nextTurn&&!collide_flag) {
			next_turn_wait += Time.deltaTime;
		}
		if (next_turn_wait > 3f &&!collide_flag) {
   //		brickselected.GetComponent<Brick> ().selected = false;
			gameController.SendMessage ("NewTurn");
			GameObject[] selectors = GameObject.FindGameObjectsWithTag ("Selector");
			foreach (GameObject sel in selectors) {
	//			sel.GetComponent<ToolBar_select> ().selected_brick = null;
	//			sel.GetComponent<ToolBar_select> ().try_brick = null;
				sel.GetComponent<ToolBar_select> ().select_flag = false;
				sel.GetComponent<ToolBar_select> ().waitTime = 0f;
			}
			nextTurn = false;
			next_turn_wait = 0f;
		}
		Debug.Log (next_turn_wait);
   	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bricks")
        {
			if (!collision.gameObject.GetComponent<Brick> ().selected)
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    Vector3 offset = contact.point - transform.position;

					if (offset.magnitude > 3.5) {
						GameOver.enabled = true;
						//GameObject.Find ("GameController").SetActive (false);
						gameController.SendMessage ("EndTurn");
						collide_flag = true;
						nextTurn = false;
						next_turn_wait = 0f;
					}
                }
			}
			else if (collision.gameObject.GetComponent<Brick> ().selected&&collision.transform.parent.tag!="Selector"){
				collide_flag = true;
				nextTurn = false;
				next_turn_wait = 0f;
				Debug.Log("Please Repick the brick");
				message.enabled = true;
				timer = 0;
				collision.gameObject.GetComponent<Brick> ().selectable = true;
				collision.gameObject.GetComponent<Renderer>().material=collision.gameObject.GetComponent<Brick> ().hightlight_material;
			}

		}
		else {
			nextTurn = false;
			next_turn_wait = 0f;
		}
	}

	public void NextTurn(){
		next_turn_wait = 0f;
		nextTurn = true;
	
	}
}

