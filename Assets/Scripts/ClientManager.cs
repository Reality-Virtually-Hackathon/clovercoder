using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ClientManager : NetworkManager {
    //Server Side

	private static bool flag = true;

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
        Debug.Log("Starting Client");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
		if (flag) {
			var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
			flag = false;
		}
    }

    //when client recieves password information from the server.
    public void OnReceivePassword(NetworkMessage netMsg)
    {
        //read the server password.
        var msg = netMsg.ReadMessage<StringMessage>().value;
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