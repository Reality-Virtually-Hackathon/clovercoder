using UnityEngine;
using UnityEngine.Networking;

public class NetworkMenuHUD : MonoBehaviour
{
	public NetworkManager manager;
	[SerializeField] public bool showGUI = true;
	[SerializeField] public int offsetX;
	[SerializeField] public int offsetY;

	// Runtime variable
	bool showServer = false;

	void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}

	void Update()
	{
		if (!showGUI)
			return;

		if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				manager.StartServer();
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				manager.StartHost();
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				manager.StartClient();
			}
		}
		if (NetworkServer.active && NetworkClient.active)
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				manager.StopHost();
			}
		}
	}

	public Rect fiveTimes(Rect rect){
		rect.xMin *= 5;
		rect.yMin *= 5;
		rect.xMax *= 5;
		rect.yMax *= 5;
		return rect;
	}

	void OnGUI()
	{
		if (!showGUI)
			return;

		int xpos = 5 * (10 + offsetX);
		int ypos = 5 * (40 + offsetY);
		int spacing = 50;
		//GUI.skin.label.fontSize = GUI.skin.button.fontSize = 20;
		if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
		{
			if (GUI.Button(fiveTimes(new Rect(xpos, ypos, 200, 20)), "LAN Host(H)"))
			{
				manager.StartHost();
			}
			ypos += spacing;

			if (GUI.Button(fiveTimes(new Rect(xpos, ypos, 105, 20)), "LAN Client(C)"))
			{
				manager.StartClient();
			}
			manager.networkAddress = GUI.TextField(fiveTimes(new Rect(xpos + 100, ypos, 95, 20)), manager.networkAddress);
			ypos += spacing;
		}
		else
		{
			if (NetworkServer.active)
			{
				GUI.Label(fiveTimes(new Rect(xpos, ypos, 300, 20)), "Server: port=" + manager.networkPort);
				ypos += spacing;
			}
			if (NetworkClient.active)
			{
				GUI.Label(fiveTimes(new Rect(xpos, ypos, 300, 20)), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
				ypos += spacing;
			}
		}

		if (NetworkClient.active && !ClientScene.ready)
		{
			if (GUI.Button(fiveTimes(new Rect(xpos, ypos, 200, 20)), "Client Ready"))
			{
				ClientScene.Ready(manager.client.connection);

				if (ClientScene.localPlayers.Count == 0)
				{
					ClientScene.AddPlayer(0);
				}
			}
			ypos += spacing;
		}

		if (NetworkServer.active || NetworkClient.active)
		{
			GUI.Button (fiveTimes (new Rect (xpos, ypos, 200, 20)), "Host ID : "+NetworkServer.serverHostId); 
			ypos += spacing;
			if (GUI.Button(fiveTimes(new Rect(xpos, ypos, 200, 20)), "Stop (X)"))
			{
				manager.StopHost();
			}
			ypos += spacing;
		}
	}
}