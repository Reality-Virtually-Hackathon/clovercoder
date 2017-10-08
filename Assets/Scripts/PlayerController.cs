using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {
	
	void Start () {
	}
    
    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown (1)) {
			GetComponent<Renderer>().material.color = Color.blue;
			if (isServer) {
				RpcChanges ();
			} else {
				CmdUpdate ();
			}
		}
	}

	[Command]
	void CmdUpdate () {
		GetComponent<Renderer>().material.color = Color.blue;
	}

	[ClientRpc]
	void RpcChanges() {
		GetComponent<Renderer>().material.color = Color.blue;
	}
}