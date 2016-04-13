using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartUp : MonoBehaviour {
	public string server_ip;
	public int server_port;
	public bool isServer = false;
	public bool started = false;
	// Use this for initialization
	void Start () {
		if (server_ip.Equals (Network.player.ipAddress)) {
			Debug.Log ("This is server");
			isServer = true;
		} else {
			Debug.Log ("This is client");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			if (isServer) {
				NetworkManager.singleton.networkPort = server_port;
				NetworkManager.singleton.StartHost ();
			} else {
				NetworkManager.singleton.networkAddress = server_ip;
				NetworkManager.singleton.networkPort = server_port;
				NetworkManager.singleton.StartClient ();
			}
			started = true;
		}
	}
}
