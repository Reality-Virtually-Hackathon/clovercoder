using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using System;

public class MyNetworkManager : NetworkManager {
	
	//private static Boolean flag = true;

	//Server Side

	public override void OnStartServer()
	{
		base.OnStartServer();
		RegisterServerHandles();
	}

	//keeping track of Clients connecting.
	public override void OnServerConnect(NetworkConnection conn)
	{
		base.OnServerConnect(conn);
	}

	//keeping track of Clients disconnecting.
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		base.OnServerDisconnect(conn);
	}

	public override void OnStopServer()
	{
		base.OnStopServer();
	}

	//Client Side
	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
		RegisterClientHandles();
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
	}

	/*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		//Debug.Log ("on server add player called");
		if (flag) {
			//playerPrefab = (GameObject)Resources.Load ("Player");
			base.OnServerAddPlayer (conn, playerControllerId);
			flag = false;
		}
		else {
			playerPrefab = (GameObject)Resources.Load ("Cube");
		}
		ClientScene.RegisterPrefab (playerPrefab);
		Debug.Log ("Registering Prefab");
		GameObject player = (GameObject)GameObject.Instantiate (playerPrefab, transform.position, transform.rotation);
		NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
	}*/

	//when client recieves password information from the server.
	public void OnReceivePassword(NetworkMessage netMsg)
	{
		//read the server password.
		var msg = netMsg.ReadMessage<StringMessage>().value;
		//serverPassword = msg;
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		base.OnClientDisconnect(conn);
	}


	//Messages that need to be Registered on Server and Client Startup.
	void RegisterServerHandles()
	{
		NetworkServer.RegisterHandler(MsgType.Highest + 1, OnReceivePassword);
	}

	void RegisterClientHandles()
	{
		client.RegisterHandler(MsgType.Highest + 1, OnReceivePassword);
	}
}